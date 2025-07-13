using System;
using Northwind.Common.EntityModels.SqlServer.Models;

namespace Northwind.GraphQL.Service.Models;

public class UpdateProductPayload
{
    public UpdateProductPayload(Product? product, bool updated)
    {
        Product = product;
        Success = updated;
    }

    public Product? Product { get; }
    public bool Success { get; }
}
