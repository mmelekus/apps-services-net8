using Microsoft.AspNetCore.Mvc;   // To use [Route], [ApiController], [HttpGet] and so on.
using Northwind.Common.DataContext.SqlServer; // To use NorthWindDb context
using Northwind.Common.EntityModels.SqlServer.Models; // To use Product

namespace Northwind.WebApi.Service.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private int _pageSize = 10;
    private readonly ILogger<ProductsController> _logger;
    private readonly NorthwindDb _db;

    public ProductsController(ILogger<ProductsController> logger, NorthwindDb context)
    {
        _logger = logger;
        _db = context;
    }

    // GET: api/products
    [HttpGet]
    [Produces(typeof(Product[]))]
    public IEnumerable<Product> Get (int? page) => _db.Products
                                                      .Where(p => p.UnitsInStock > 0 && !p.Discontinued)
                                                      .OrderBy(product => product.ProductId)
                                                      .Skip(((page ?? 1) - 1) * _pageSize)
                                                      .Take(_pageSize);

    // GET: api/proudcts/outofstock
    [HttpGet]
    [Route("outofstock")]
    [Produces(typeof(Product[]))]
    public IEnumerable<Product> GetOutOfStockProducts() => _db.Products
                                                              .Where(p => p.UnitsInStock == 0 && !p.Discontinued);

    // GET: api/products/discontinued
    [HttpGet]
    [Route("discontinued")]
    [Produces(typeof(Product[]))]
    public IEnumerable<Product> GetDiscontinuedProducts() => _db.Products.Where(product => product.Discontinued);

    // GET: api/products/5
    [HttpGet("{id:int}")]
    public async ValueTask<Product?> Get(int id) => await _db.Products.FindAsync(id);

    // GET: api/products/cha
    [HttpGet("{name}")]
    public IEnumerable<Product> Get(string name) => _db.Products.Where(p => p.ProductName.Contains(name));

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return Created($"api/products/{product.ProductId}", product);
    }
}
