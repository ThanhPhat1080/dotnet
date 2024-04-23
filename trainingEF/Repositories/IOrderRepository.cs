using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> CreateOrder(OrderRequestDto requestOrder, UserDto currentUser);
    Task<Order?> GetOrderById(string id);

    Task<OrderDetail?> CreateOrderDetail(OrderDetail orderDetail);
}