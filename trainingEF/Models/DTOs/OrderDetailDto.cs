
namespace trainingEF.Models.DTOs;

public class OrderDetailDto
{
    public OrderDetailDto(OrderDetail orderDetail)
    {
        Id = orderDetail.Id;
        ProductId = orderDetail.ProductId;
        Quantity = orderDetail.Quantity;
        OrderId = orderDetail.OrderId;
    }

    public string Id { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
    public string OrderId { get; set; } = null!;
}