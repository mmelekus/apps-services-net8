namespace Northwind.EntityModels;

public record Cart(
    Customer Customer,
    List<LineItem> Items
);