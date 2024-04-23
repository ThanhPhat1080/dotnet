using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainingEF.Models;

public class Product
{
    [Key] // Set primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = "";

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
}
