using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // DbContext

namespace Northwind.Common.DataContext.SqlServer;

public partial class NorthwindDb : DbContext
{
    private static readonly SetLastRefreshedInterceptor setLastRefreshedInterceptor = new();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = "tcp:127.0.0.1,1433";
            builder.InitialCatalog = "Northwind";
            builder.TrustServerCertificate = true;
            builder.MultipleActiveResultSets = true;
            // Because we want to fail fast.  Default is 15 seconds.
            builder.ConnectTimeout = 3;
            // If using Windows Integrated authentication.
            // builder.IntegratedSecurity = true;
            // If using SQL Server authentication.
            builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
            builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }

        optionsBuilder.AddInterceptors(setLastRefreshedInterceptor);
    }
}
