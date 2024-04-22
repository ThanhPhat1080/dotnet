using trainingEF.Models.DTOs;

namespace trainingEF.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderPlaced { get; set; }

    public DateTime Orderfulfilled { get; set; }

    public int UserId { get; set; }
    public UserDto? Customer { get; set; } = null;

    public ICollection<OrderDetail>? OrderDetails { get; set; } = null;
}
