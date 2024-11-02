using FluentValidation.Models; // To use Order
using FluentValidation.Results; // To use ValidationResult.
using FluentValidation.Validators; // To use OrderValidator.

using static System.Console; // To use OutputEncoding.
using System.Globalization; // To use CultureInfo.
using System.Text; // to use Encoding.

OutputEncoding = Encoding.UTF8; // Enable Euro symbol.
// Control the culture used for formatting of dates and currency,
// and for localizing error messages to local language.
Thread t = Thread.CurrentThread;
t.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
// t.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
t.CurrentUICulture = t.CurrentCulture;
WriteLine($"Current culture: {t.CurrentCulture.DisplayName}");
WriteLine();

Order order = new()
{
    OrderId = 10001,
    CustomerName = "Abcdef", // Abc
    CustomerEmail = "abc@example.com", // "abc&example.com"
    CustomerLevel = CustomerLevel.Gold, // (CustomerLevel)4
    OrderDate = new(2022, month: 12, day: 1),
    ShipDate = new(2022, month: 12, day: 5),
    Total = 49.99M
};

OrderValidator validator = new();
ValidationResult result = validator.Validate(order);

// Output the order data.
WriteLine($"CustomrName:   {order.CustomerName}");
WriteLine($"CustomerEmail: {order.CustomerEmail}");
WriteLine($"CustomerLevel: {order.CustomerLevel}");
WriteLine($"OrderId:       {order.OrderId}");
WriteLine($"OderDate:      {order.OrderDate}");
WriteLine($"ShipDate:      {order.ShipDate}");
WriteLine($"Total:         {order.Total:C}");
WriteLine();

// Output if the order is valid and any rules that were broken.
WriteLine($"IsValid:  {result.IsValid}");
foreach(ValidationFailure item in result.Errors)
{
    WriteLine($"  {item.Severity}: {item.ErrorMessage}");
}