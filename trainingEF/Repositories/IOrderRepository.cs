using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllOrders();
    public Task<Order> CreateOrder(OrderRequestDto requestOrder, UserDto currentUser);
}