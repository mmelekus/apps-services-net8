using System;

namespace Northwind.GraphQL.Service.Models;

public class DeleteProductPayload
{
    public DeleteProductPayload(bool deleted)
    {
        Success = deleted;
    }

    public bool Success { get; }
}
