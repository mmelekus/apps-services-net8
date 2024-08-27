using Microsoft.Azure.Cosmos; // To use CosmosClient and so on.
using Microsoft.Azure.Cosmos.Scripts; // To use StoredProcedures and so on.
using System.Net; // To use HttpStatusCode.
using Northwind.Common.EntityModels.SqlServer.Models; // To use NorthwindContext and so on.
using Northwind.CosmosDb.SqlApi.Models; // To use ProductCosmos and so on.
using Microsoft.EntityFrameworkCore;
using Northwind.Common.DataContext.SqlServer; // To us Include extension method.

partial class Program
{
    // To use Azure Cosmos DB in the local emulator.
    private static string endpointUri = "https://localhost:8081/";
    private static string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    private static CosmosClientOptions options = new()
    {
        HttpClientFactory = () => new HttpClient(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        }),
        ConnectionMode = ConnectionMode.Gateway
    };

    /*
    // To use Azure Cosmos DB in the cloud.
    private static string account = "apps-services-books"; // use your account
    private static string endpointUri = $"https://{account}.documents.azure.com:443/";
    private static string primaryKey = "LGrz7H...gZw=="; // use your key
    */

    static async Task CreateCosmosResources()
    {
        SectionTitle("Creating Cosmos Resources");

        try
        {
            using (CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey,
                clientOptions: options
            ))
            {
                DatabaseResponse dbResponse = await client.CreateDatabaseIfNotExistsAsync("Northwind", throughput: 400 /* RU/s */);
                string status = dbResponse.StatusCode switch
                {
                    HttpStatusCode.OK => "exists",
                    HttpStatusCode.Created => "created",
                    _ => "unknown"
                };
                WriteLine($"Database Id: {dbResponse.Database.Id}, Status: {status}");

                IndexingPolicy indexingPolicy = new()
                {
                    IndexingMode = IndexingMode.Consistent,
                    Automatic = true, // Items are indexed unless explicitly excluded.
                    IncludedPaths = { new IncludedPath { Path = "/*" } }
                };

                ContainerProperties containerProperties = new("Products", partitionKeyPath: "/productId")
                {
                    IndexingPolicy = indexingPolicy
                };
                ContainerResponse containerResponse = await dbResponse.Database.CreateContainerIfNotExistsAsync(containerProperties, throughput: 1000 /* RU/s */);
                status = dbResponse.StatusCode switch
                {
                    HttpStatusCode.OK => "exists",
                    HttpStatusCode.Created => "created",
                    _ => "unknown"
                };
                WriteLine($"Container Id: {containerResponse.Container.Id}, Status: {status}");

                Container container = containerResponse.Container;
                ContainerProperties properties = await container.ReadContainerAsync();
                WriteLine($"  PartitionKeyPath: {properties.PartitionKeyPath}");
                WriteLine($"  LastModified: {properties.LastModified}");
                WriteLine($"  IndexingPolicy.IndexingMode: {properties.IndexingPolicy.IndexingMode}");
                WriteLine($"  IndexingPolicy.IncludedPaths: {string.Join(",", properties.IndexingPolicy.IncludedPaths.Select(path => path.Path))}");
                WriteLine($"  IndexingPolicy: {properties.IndexingPolicy}");
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: if you are using the Azure Cosmos Emulator then please make sure that it is running.");
        }
        catch (Exception ex)
        {
            WriteLine($"Error: {ex.GetType()} says {ex.Message}");
        }
    }

    static async Task CreateProductItems()
    {
        SectionTitle("Creating Product Items");

        double totalCharge = 0.0;

        try
        {
            using (CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey,
                clientOptions: options
            ))
            {
                Container container = client.GetContainer(databaseId: "Northwind", containerId: "Products");
                
                using (NorthwindDb db = new())
                {
                    if (!db.Database.CanConnect())
                    {
                        WriteLine($"Cannot connect to the SQL Server database to read products using database connection string: {db.Database.GetConnectionString()}");
                        return;
                    }

                    ProductCosmos[] products = db.Products
                        // Get the related data for embedding.
                        .Include(p => p.Category)
                        .Include(p => p.Supplier)
                        // Filter any products with null category or supplier to avoid null warnings
                        .Where(p => (p.Category != null) && (p.Supplier != null))
                        // Project the EF Core entities into Cosmos JSON types.
                        .Select(p => new ProductCosmos
                        {
                            id = p.ProductId.ToString(),
                            productId = p.ProductId.ToString(),
                            productName = p.ProductName,
                            quantityPerUnit = p.QuantityPerUnit,
                            // If the related category is null, store null, else store the category mapped to Cosmos model.
                            category = p.Category == null ? null : new CategoryCosmos
                            {
                                categoryId = p.Category.CategoryId,
                                categoryName = p.Category.CategoryName,
                                description = p.Category.Description,
                            },
                            supplier = p.Supplier == null ? null : new SupplierCosmos
                            {
                                supplierId = p.Supplier.SupplierId,
                                companyName = p.Supplier.CompanyName,
                                contactName = p.Supplier.ContactName,
                                contactTitle = p.Supplier.ContactTitle,
                                address = p.Supplier.Address,
                                city = p.Supplier.City,
                                country = p.Supplier.Country,
                                postalCode = p.Supplier.PostalCode,
                                region = p.Supplier.Region,
                                phone = p.Supplier.Phone,
                                fax = p.Supplier.Fax,
                                homePage = p.Supplier.HomePage,
                            },
                            unitPrice = p.UnitPrice,
                            unitsInStock = p.UnitsInStock,
                            reorderLevel = p.ReorderLevel,
                            unitsOnOrder = p.UnitsOnOrder,
                            discontinued = p.Discontinued,
                        })
                        .ToArray();
                    
                    foreach (ProductCosmos product in products)
                    {
                        try
                        {
                            // Try to read the item to see if it exists.
                            ItemResponse<ProductCosmos> productResponse = await container.ReadItemAsync<ProductCosmos>(
                                id: product.id,
                                new PartitionKey(product.productId)
                            );
                            WriteLine($"Item with id: {productResponse.Resource.id} exists.  Query consumed {productResponse.RequestCharge} RUs.");
                            totalCharge += productResponse.RequestCharge;
                        }
                        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                        {
                            // Create the item if it does not exist.
                            ItemResponse<ProductCosmos> productResponse = await container.CreateItemAsync(product);
                            WriteLine($"Created item with id: {productResponse.Resource.id}. Insert consumed {productResponse.RequestCharge} RUs.");
                            totalCharge += productResponse.RequestCharge;
                        }
                        catch (Exception ex)
                        {
                            WriteLine($"Error: {ex.GetType()} says {ex.Message}");
                        }
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
        }
        catch (Exception ex)
        {
            WriteLine($"Error: {ex.GetType()} says {ex.Message}");
        }
        WriteLine($"Total requests charge: {totalCharge:N2} RUs.");
    }

    static async Task ListProductItems(string sqlText = "SELECT * FROM c")
    {
        SectionTitle("Listing Product Items");

        try
        {
            using (CosmosClient client = new(accountEndpoint: endpointUri, authKeyOrResourceToken: primaryKey, clientOptions: options))
            {
                Container container = client.GetContainer(databaseId: "Northwind", containerId: "Products");
                
                WriteLine($"Running query: {sqlText}");
                QueryDefinition query = new(sqlText);
                using FeedIterator<ProductCosmos> resultsIterator = container.GetItemQueryIterator<ProductCosmos>(query);
                if (!resultsIterator.HasMoreResults) { WriteLine("No results found."); }
                while (resultsIterator.HasMoreResults)
                {
                    FeedResponse<ProductCosmos> products = await resultsIterator.ReadNextAsync();
                    WriteLine($"Status code: {products.StatusCode}, Request charge: {products.RequestCharge} RUs.");
                    WriteLine($"{products.Count} products found.");
                    foreach (ProductCosmos product in products)
                    {
                        WriteLine($"id: {product.id}, productName: {product.productName}, unitPrice: {product.unitPrice:C}");
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
        }
        catch (Exception ex)
        {
            WriteLine($"Error {ex.GetType()} says {ex.Message}");
        }
    }

    static async Task DeleteProductItems()
    {
        SectionTitle("Deleting Product Items");

        double totalCharge = 0.0;

        try
        {
            using (CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey,
                clientOptions: options
            ))
            {
                Container container = client.GetContainer(databaseId: "Northwind", containerId: "Products");
                string sqlText = "SELECT * FROM c";
                WriteLine($"Running query: {sqlText}");
                QueryDefinition query = new(sqlText);
                using FeedIterator<ProductCosmos> resultsIterator = container.GetItemQueryIterator<ProductCosmos>(query);
                while (resultsIterator.HasMoreResults)
                {
                    FeedResponse<ProductCosmos> products = await resultsIterator.ReadNextAsync();
                    foreach (ProductCosmos product in products)
                    {
                        WriteLine($"Delete id: {product.id}, productName: {product.productName}");
                        ItemResponse<ProductCosmos> response = await container.DeleteItemAsync<ProductCosmos>(id: product.id, partitionKey: new(product.id));
                        WriteLine($"Status code: {response.StatusCode}, Request charge: {response.RequestCharge}");
                        totalCharge += response.RequestCharge;
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
        }
        catch (Exception ex)
        {
            WriteLine($"Error: {ex.GetType()} says {ex.Message}");
        }

        WriteLine($"Total requests charge: {totalCharge:N2} RUs");
    }

    static async Task CreateInsertProductStoredProcedure()
    {
        SectionTitle("Creating the insertProduct stored procedure");

        try
        {
            using (CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey,
                clientOptions: options
            ))
            {
                Container container = client.GetContainer(databaseId: "Northwind", containerId: "Products");
                StoredProcedureResponse response = await container
                    .Scripts
                    .CreateStoredProcedureAsync(new StoredProcedureProperties
                    {
                        Id = "insertProduct",
                        // __ means getContext().getCollection().
                        Body = """
function insertProduct(product) {
    if (!product) throw new Error("product is undefined or null.");
    tryInsert(product, callbackInsert);
    function tryInsert(product, callbackFunction) {
        var options = { disableAutomaticIdGeneration: false};
        // __ is an alias for getContext().getCollection()
        var isAccepted = __.createDocument(
            __.getSelfLink(),
            product,
            options,
            callbackFunction
        );
        if (!isAccepted) getContext().getResponse().setBody(1);
    }
    function callbackInsert(err, item, options) {
        if (err) throw err;
        getContext().getResponse().setBody(1);
    }
}
"""
                    });
                WriteLine($"Status code: {response.StatusCode}, Request charge: {response.RequestCharge} RUs.");
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
        }
        catch (Exception ex)
        {
            WriteLine($"Error: {ex.GetType()} says {ex.Message}");
        }
    }

    static async Task ExecuteInsertProductStoredProcedure()
    {
        SectionTitle("Executing the insertProduct stored procedure");

        try
        {
            using (CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey,
                clientOptions: options
            ))
            {
                Container container = client.GetContainer(databaseId: "Northwind", containerId: "Products");
                string pid = "78";
                ProductCosmos product = new()
                {
                    id = pid,
                    productId = pid,
                    productName = "Barista's Chilli Jam",
                    unitPrice = 12M,
                    unitsInStock = 10
                };

                StoredProcedureExecuteResponse<string> response = await container.Scripts.ExecuteStoredProcedureAsync<string>("insertProduct", new PartitionKey(pid), new[] { product });
                WriteLine($"Status code: {response.StatusCode}, Request charge: {response.RequestCharge} RUs.");
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"Error: {ex.Message}");
            WriteLine("Hint: If you are using the Azure Cosmos Emulator then please make sure it is running.");
        }
        catch (Exception ex)
        {
            WriteLine("Error: {0} says {1}",
            arg0: ex.GetType(),
            arg1: ex.Message);
        }
    }
}