using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using trainingEF.Data;
using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext appDbContext;
    private readonly DbSet<Order> orderDbSet;
    private readonly IIdentityRepository identityRepository;

    public OrderRepository(
        AppDbContext context,
        IIdentityRepository _identityRepository)
    {
        appDbContext = context;
        orderDbSet = context.OrderDbSet;
        identityRepository = _identityRepository;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        List<Order> orders = await orderDbSet.ToListAsync();

        return orders;
    }

    public async Task<Order> CreateOrder(OrderRequestDto requestOrder, UserDto currentUser)
    {
        Order newOrder = new()
        {
            Id = new Guid().ToString(),
            OrderPlaced = requestOrder.OrderPlaced,
            OrderFulfilled = requestOrder.OrderFulfilled,

            UserId = currentUser.Id,
            User = currentUser,

            //OrderDetails = Array.Empty<OrderDetail>()
            OrderDetails = requestOrder.OrderDetails
        };

        await orderDbSet.AddAsync(newOrder);
        await appDbContext.SaveChangesAsync();

        return newOrder;
    }
}
