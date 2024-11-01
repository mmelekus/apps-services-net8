namespace Northwind.EntityModels;

public record LineItem(
    string ProductName,
    decimal UnitPrice,
    int Quantity
);