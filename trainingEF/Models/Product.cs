using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainingEF.Models.DTOs;

namespace trainingEF.Models;

public class Product
{
    public Product()
    {
    }

    public Product(ProductDto productDto)
    {
        Id = productDto.Id;
        Name = productDto.Name;
        Price = productDto.Price;
    }

    [Key] // Set primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = "";

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
