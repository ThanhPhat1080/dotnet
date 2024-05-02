using trainingEF.Models;
using trainingEF.Models.DTOs;
using trainingEF.Repositories;

namespace trainingEF.Services;

public class ProductServices : IProductService
{
    private readonly IProductRepository productRepository;
    public ProductServices(IProductRepository _productRepository)
    {
        productRepository = _productRepository;
    }

    public async Task<ProductDto> CreateProduct(ProductDto productDto)
    {
        Product newProduct = new(productDto);
        Product product = await productRepository.CreateProduct(newProduct);

        return new(product);
    }
}
