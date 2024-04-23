using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using trainingEF.Models;
using trainingEF.Models.DTOs;
using trainingEF.Repositories;

namespace trainingEF.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository orderRepository;
    private readonly IIdentityRepository identityRepository;

    public OrderController(IOrderRepository _orderRepository, IIdentityRepository _identityRepository)
    {
        orderRepository = _orderRepository;
        identityRepository = _identityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok(await orderRepository.GetAllOrders());
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderRequest)
    {
        string userId = User.FindFirstValue("Id");
        UserDto? currentUser = await identityRepository.GetUserById(userId);

        if (currentUser == null)
        {
            return BadRequest("Cannot create new order");
        }

        return Ok(await orderRepository.CreateOrder(orderRequest, currentUser));
    }
}
