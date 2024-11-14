using Northwind.Common.DataContext.SqlServer; // To use the AddNorthwindContext method.
using Northwind.WebApi.Service; // To use MapGets and so on.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about cnfiguring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNorthwindContext();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGets() // Default pageSize: 10.
   .MapPosts()
   .MapPuts()
   .MapDeletes();
app.Run();
