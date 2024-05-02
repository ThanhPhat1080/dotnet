using trainingEF.Models.DTOs;

namespace trainingEF.Services;

public interface IProductService
{
    Task<ProductDto> CreateProduct(ProductDto product);
}