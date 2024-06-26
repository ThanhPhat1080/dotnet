﻿using Microsoft.AspNetCore.Mvc;
using trainingEF.Models;
using trainingEF.Repositories;

namespace trainingEF.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository _productRepository)
    {
        productRepository = _productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok(await productRepository.GetAllProducts());
    }

    [HttpGet("{id}")]
    [ActionName("GetProductDetailById")]
    public async Task<IActionResult> GetProductDetailById(string id)
    {
        Product? result = await productRepository.GetProductDetailById(id);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody]Product product)
    {
        return Ok(await productRepository.CreateProduct(product));
    }
}
