using Microsoft.Extensions.Caching.Memory;  // To use IMemoryCache and so on.
using Northwind.Common.DataContext.SqlServer;  // To use the AddNorthwindContext extenion method.
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSerilog();
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("NorthwindRedis");
    options.InstanceName = "NorthwindRedis";
});
builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(
    new MemoryCacheOptions
    {
        TrackStatistics = true,
        SizeLimit = 50 // Products
    }
));
builder.Services.AddNorthwindContext(builder.Configuration.GetConnectionString(GlobalConstants.NorthwindConnection));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
