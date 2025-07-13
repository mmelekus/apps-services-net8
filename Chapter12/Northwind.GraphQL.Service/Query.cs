using System;
using Northwind.Common.EntityModels.SqlServer.Models;
using Northwind.Common.DataContext.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Northwind.GraphQL.Service;

public class Query
{
    public string GetGreeting() => "Hello, World!";
    public string Farewell() => "Ciao! Ciao!";
    public int RollTheDie() => Random.Shared.Next(1, 7);

    public IQueryable<Category> GetCategories([Service] NorthwindDb db) => db.Categories.Include(c => c.Products);

    public Category? GetCategory([Service] NorthwindDb db, int categoryId)
    {
        Category? category = db.Categories.Find(categoryId);
        if (category == null) { return null; }
        db.Entry(category).Collection(c => c.Products).Load();
        return category;
    }

    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts([Service] NorthwindDb db) => db.Products.Include(p => p.Category);

    public IQueryable<Product> GetProductsInCategory([Service] NorthwindDb db, int categoryId) => db.Products.Where(p => p.CategoryId == categoryId);

    public IQueryable<Product> GetProductsByUnitPrice([Service] NorthwindDb db, decimal minimumUnitPrice) => db.Products.Where(p => p.UnitPrice >= minimumUnitPrice);

    [UsePaging]
    public IQueryable<Product> GetProductsWithPaging([Service] NorthwindDb db) => db.Products.Include(p => p.Category);
}
