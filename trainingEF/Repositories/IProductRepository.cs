using trainingEF.Models;

namespace trainingEF.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
}
