using GeneratingPdf.Document; // To use CatalogDocument
using GeneratingPdf.Models; // To us Catalog, Category
using QuestPDF.Fluent; // To use the GeneratePdf extension method.
using QuestPDF.Infrastructure; // To use LicenseType.
using static System.Console; // To use WriteLine.

QuestPDF.Settings.License = LicenseType.Community;
string fileName = "catalog.pdf";
Catalog model = new()
{
    Categories = new()
    {
        new() { CategoryId = 1, CategoryName = "Beverages"},
        new() { CategoryId = 2, CategoryName = "Condiments"},
        new() { CategoryId = 3, CategoryName = "Confections"},
        new() { CategoryId = 4, CategoryName = "Dairy Products"},
        new() { CategoryId = 5, CategoryName = "Grains/Cereals"},
        new() { CategoryId = 6, CategoryName = "Meat/Poultry"},
        new() { CategoryId = 7, CategoryName = "Produce"},
        new() { CategoryId = 8, CategoryName = "Seafood"}
    }
};
CatalogDocument document = new(model);
document.GeneratePdf(fileName);
WriteLine($"PDF catalog has been created: {Path.Combine(Environment.CurrentDirectory, fileName)}");

try
{
    if (OperatingSystem.IsWindows()) { System.Diagnostics.Process.Start("explorer.exe", fileName); }
    else { WriteLine("Open the file manually."); }
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
}