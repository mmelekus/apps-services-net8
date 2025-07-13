namespace Northwind.GraphQL.Service.Models;

public record class UpdateProductPriceInput(
    int? ProductId,
    decimal? UnitPrice
);