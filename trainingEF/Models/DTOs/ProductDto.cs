using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trainingEF.Models.DTOs;

public class ProductDto
{
    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        OrderDetails = product.OrderDetails
            .Select(x => new OrderDetailDto(x))
            .ToList();
    }

    public string Id { get; set; } = null!;
    public string Name { get; set; } = "";
    public decimal Price { get; set; }

    public ICollection<OrderDetailDto> OrderDetails { get; } = new List<OrderDetailDto>();
}
