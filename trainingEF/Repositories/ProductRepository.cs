using Microsoft.EntityFrameworkCore;
using System;
using trainingEF.Data;
using trainingEF.Models;

namespace trainingEF.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext appDbContext;
    private readonly DbSet<Product> productDbSet;

    public ProductRepository(AppDbContext context)
    {
        appDbContext = context;
        productDbSet = context.ProductDbSet;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        Product newProduct = new()
        {
            Name = product.Name,
            Price = product.Price
        };

        await productDbSet.AddAsync(newProduct);
        await appDbContext.SaveChangesAsync();

        return newProduct;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await productDbSet.ToListAsync();
    }

    public async Task<Product?> GetProductDetailById(string id)
    {
        Product? result = await productDbSet
            .Include(x => x.OrderDetails)
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }
}
