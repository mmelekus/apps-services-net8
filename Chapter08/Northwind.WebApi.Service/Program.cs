using Northwind.Common.DataContext.SqlServer; // To use the AddNorthwindContext method.
using Northwind.WebApi.Service; // To use MapGets and so on.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(defaultScheme: "Bearer")
    .AddJwtBearer();
builder.Services.AddCustomRateLimiting(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNorthwindContext();
builder.Services.AddCustomHttpLogging();

WebApplication app = builder.Build();

// Configure authorization
app.UseAuthorization();

// Configure CORS
app.UseCors(policyName: "Northwind.Mvc.Policy");
// app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpLogging();
await app.UseCustomClientRateLimiting();

app.MapGets() // Default pageSize: 10.
   .MapPosts()
   .MapPuts()
   .MapDeletes();

app.Run();
