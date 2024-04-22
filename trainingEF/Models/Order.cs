using trainingEF.Models.DTOs;

namespace trainingEF.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderPlaced { get; set; }
    public DateTime OrderFulfilled { get; set; }

    public string UserId { get; set; } = null!;
    public UserDto User { get; set; } = null!;

    public ICollection<OrderDetail> OrderDetails { get; set; } = null!;
}
