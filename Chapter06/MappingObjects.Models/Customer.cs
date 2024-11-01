namespace Northwind.EntityModels;

/// <summary>
/// This record will only have a constructor with the paramters below.
/// Objects will be immutable after instantiation using this constructor.
/// It will not have a default parameterless constructor.
/// </summary>
public record Customer(
    string FirstName,
    string LastName
);