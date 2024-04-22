using Microsoft.EntityFrameworkCore;
using trainingEF.Data;
using trainingEF.Models;

namespace trainingEF.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbSet<Order> orderDbSet;

    public OrderRepository(AppDbContext context)
    {
        orderDbSet = context.OrderDbSet;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        List<Order> orders = await orderDbSet.ToListAsync();

        return orders;
    }
}
