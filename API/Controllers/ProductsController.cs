using System;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext dbContext;

    public ProductsController(StoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllAsync()
    {
        return await dbContext.Products.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }
    [HttpPost("{productName}")]
    public async Task<IActionResult> CreateProduct(string productName)
    {
        var p=new Product{Name=productName};
        await dbContext.Products.AddAsync(p);
        return CreatedAtAction(nameof(GetById),new{Id=p},p);
    }
}
