using System;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository repo;

    public ProductsController(IProductRepository repo)
    {
        this.repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllAsync()
    {
        var products= await repo.GetProductsAsync();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product= await repo.GetProductByIdAsync(id);
        return Ok(product);
    }
   
}
