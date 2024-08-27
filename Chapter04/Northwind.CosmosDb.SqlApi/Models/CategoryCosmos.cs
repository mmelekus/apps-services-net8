namespace Northwind.CosmosDb.SqlApi.Models;

public class CategoryCosmos
{
    public int categoryId { get; set; }
    public string categoryName { get; set; } = null!;
    public string? description { get; set; }
}
