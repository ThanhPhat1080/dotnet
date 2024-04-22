using trainingEF.Models;

namespace trainingEF.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllOrders();
}