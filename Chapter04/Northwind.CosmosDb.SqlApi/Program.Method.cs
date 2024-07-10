using Microsoft.Azure.Cosmos; // To use CosmosClient and so on.
using System.Net; // To use HttpStatusCode.

partial class Program
{
    // To use Azure Cosmos DB in the local emulator.
    private static string endpointUri = "https://localhost:8081/";
    private static string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
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
            CosmosClientOptions options = new()
            {
                HttpClientFactory = () => new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }),
                ConnectionMode = ConnectionMode.Gateway
            };

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
}