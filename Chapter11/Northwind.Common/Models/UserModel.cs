using System;

namespace Northwind.Common.Models;

public class UserModel
{
    public string Name { get; set; } = null!;
    public string ConnectionId { get; set; } = null!;
    public string? Groups { get; set; }
}
