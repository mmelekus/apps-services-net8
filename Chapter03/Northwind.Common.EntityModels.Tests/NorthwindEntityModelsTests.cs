using Northwind.Common.DataContext.SqlServer;
using Northwind.Common.EntityModels.SqlServer.Models;

namespace Northwind.Common.EntityModels.Tests;

public class NorthwindEntityModelsTests
{
    [Fact]
    public void CanConnectIsTrue()
    {
        using (NorthwindDb db = new()) // arrange
        {
            bool canConnect = db.Database.CanConnect(); // act
            Assert.True(canConnect); // assert
        }
    }

    [Fact]
    public void ProviderIsSqlServer()
    {
        using(NorthwindDb db = new())
        {
            string? provider = db.Database.ProviderName;
            Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", provider);
        }
    }

    [Fact]
    public void ProductId1IsChai()
    {
        using(NorthwindDb db = new())
        {
            Product product1 = db.Products.Single(p => p.ProductId == 1);
            Assert.Equal("Chai", product1.ProductName);
        }
    }

    [Fact]
    public void EmployeeHasLastRefreshedIn10sWindow()
    {
        using(NorthwindDb db = new())
        {
            Employee employee1 = db.Employees.Single(p => p.EmployeeId == 1);
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Assert.InRange(actual: employee1.LastRefreshed, low: now.Subtract(TimeSpan.FromSeconds(5)), high: now.AddSeconds(5));
        }
    }
}
