using System;

namespace Northwind.GraphQL.Client.Mvc.Models;

public class ResponseErrors
{
    public Error[]? Errors { get; set; }
}
