using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainingEF.Repositories;

namespace trainingEF.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository orderRepository;

    public OrderController(IOrderRepository _orderRepository)
    {
        orderRepository = _orderRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(orderRepository.GetAllOrders());
    }
}
