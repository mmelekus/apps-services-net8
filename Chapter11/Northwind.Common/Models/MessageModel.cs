using System;

namespace Northwind.Common.Models;

public class MessageModel
{
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public string? Body { get; set; }
}
