using Microsoft.AspNetCore.Mvc;
using trainingEF.Models;
using trainingEF.Models.DTOs;
using trainingEF.Repositories;
using trainingEF.Services;

namespace trainingEF.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;
    private readonly IProductService productService;

    public ProductController(IProductRepository _productRepository, IProductService _productService)
    {
        productRepository = _productRepository;
        productService = _productService;
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
    public async Task<IActionResult> CreateProduct([FromBody]ProductDto product)
    {
        return Ok(await productService.CreateProduct(product));
    }
}
