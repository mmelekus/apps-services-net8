using System;

namespace Northwind.GraphQL.Service.Models;

public class ProductDiscount
{
    public int? ProductId { get; set; }
    public decimal? OriginalUnitPrice { get; set; }
    public decimal? NewUnitPrice { get; set; }
}
