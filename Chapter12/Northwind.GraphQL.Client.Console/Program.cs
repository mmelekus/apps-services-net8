using Microsoft.Extensions.DependencyInjection; // To use ServiceCollection.
using Northwind.GraphQL.Client; // To use AddNorthwindClient extension method.
using Northwind.GraphQL.Client.Console; // To use INorthwindClient.
using StrawberryShake; // To use EnsureNoErrors extension method.

ServiceCollection serviceCollection = new();
serviceCollection
    .AddNorthwindClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5121/graphql"));

IServiceProvider services = serviceCollection.BuildServiceProvider();
INorthwindClient client = services.GetRequiredService<INorthwindClient>();
var result = await client.SeafoodProducts.ExecuteAsync();
result.EnsureNoErrors();
if (result.Data is null)
{
    WriteLine("No data!");
    return;
}
foreach (var product in result.Data.ProductsInCategory)
{
    WriteLine($"{product.ProductId}: {product.ProductName}");
}