namespace trainingEF.Models.DTOs;

public class OrderDetailRequestDto
{
    public int Quantity { get; set; }
    public string ProductId { get; set; } = null!;
    public string OrderId { get; set; } = null!;
}
