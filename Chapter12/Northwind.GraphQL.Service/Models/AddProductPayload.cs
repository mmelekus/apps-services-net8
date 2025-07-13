using System;
using Northwind.Common.EntityModels.SqlServer.Models;

namespace Northwind.GraphQL.Service.Models;

public class AddProductPayload
{
    public AddProductPayload(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}
