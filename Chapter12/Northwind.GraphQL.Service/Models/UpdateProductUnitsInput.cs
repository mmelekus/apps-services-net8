namespace Northwind.GraphQL.Service.Models;

public record class UpdateProductUnitsInput(
    int? ProductId,
    short? UnitsInStock,
    short? UnitsOnOrder,
    short? ReorderLevel
);