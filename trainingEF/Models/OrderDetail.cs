using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainingEF.Models;

public class OrderDetail
{
    [Key] // Set primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    public int Quantity { get; set; }
    public string ProductId { get; set; }
    public string OrderId { get; set; }

    public Product Product { get; set; } = null!;
}
