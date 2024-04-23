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
    private readonly DbSet<OrderDetail> orderDetailDbSet;

    public OrderRepository(AppDbContext context)
    {
        appDbContext = context;
        orderDbSet = context.OrderDbSet;
        orderDetailDbSet = context.OrderDetailDbSet;
    }

    #region order
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        List<Order> orders = await orderDbSet
            .Include("User")
            .Include("OrderDetails")
            .Include("OrderDetails.Product")
            .ToListAsync();

        return orders;
    }

    public async Task<Order?> GetOrderById(string id)
    {
        Order? order = await orderDbSet
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return order;
    }

    public async Task<Order> CreateOrder(OrderRequestDto requestOrder, UserDto currentUser)
    {
        Order newOrder = new()
        {
            //Id = new Guid().ToString(),
            OrderPlaced = requestOrder.OrderPlaced,
            OrderFulfilled = requestOrder.OrderFulfilled,

            UserId = currentUser.Id,
            User = currentUser,

            OrderDetails = Array.Empty<OrderDetail>()
            //OrderDetails = requestOrder.OrderDetails
        };

        await orderDbSet.AddAsync(newOrder);
        await appDbContext.SaveChangesAsync();

        return newOrder;
    }

    #endregion

    #region order-detail

    public async Task<OrderDetail?> CreateOrderDetail(OrderDetailRequestDto requestOrder)
    {
        OrderDetail? newOrderDetail = new()
        {
            OrderId = requestOrder.OrderId,
            ProductId = requestOrder.ProductId,
            Quantity = requestOrder.Quantity
        };

        await orderDetailDbSet.AddAsync(newOrderDetail);
        await appDbContext.SaveChangesAsync();

        newOrderDetail = await orderDetailDbSet
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id.Equals(newOrderDetail.Id));

        return newOrderDetail;
    }

    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail()
    {
        return await orderDetailDbSet.ToListAsync();
    }

    #endregion
}
