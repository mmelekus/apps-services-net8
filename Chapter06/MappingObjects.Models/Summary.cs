using System.Security.Cryptography.X509Certificates;

namespace Northwind.ViewModels;

public record Summary
{
    // These properties can be initialized once, but then never changed.
    public string? FullName { get; init; }
    public decimal Total { get; init; }
    // This record class will have a default parameterless constructor.
    // The following commented statement is automatically generated:
    // public Summary() { }
}