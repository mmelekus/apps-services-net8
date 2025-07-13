using System;

namespace Northwind.GraphQL.Client.Mvc.Models;

public class Error
{
    public string Message { get; set; } = null!;
    public Location[] Locations { get; set; } = null!;
    public string[] Path { get; set; } = null!;
}
