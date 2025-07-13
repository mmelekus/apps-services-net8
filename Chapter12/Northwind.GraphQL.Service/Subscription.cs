using System;
using Northwind.GraphQL.Service.Models;

namespace Northwind.GraphQL.Service;

public class Subscription
{
    [Subscribe]
    [Topic]
    public ProductDiscount OnProductDiscounted([EventMessage] ProductDiscount productDiscount) => productDiscount;
}
