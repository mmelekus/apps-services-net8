using Northwind.GraphQL.Service; // To use Query.
using Northwind.Common.DataContext.SqlServer; // Add this if AddNorthwindContext is defined here.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNorthwindContext();
builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContextFactory<NorthwindDb>()
    .AddQueryType<Query>();

var app = builder.Build();

app.MapGet("/", () => "Navigate to: https://localhost:5121/graphql");

app.MapGraphQL();

await app.RunAsync();
