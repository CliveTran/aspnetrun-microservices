using Microsoft.EntityFrameworkCore;
using order.application.Contracts.Repositories;
using order.domain.Entities;
using order.infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace order.infrastructure.Repositories
{
    public class OrderRepository : AsyncRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        { }

        public async Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName)
        {
            return await _context.Orders.Where(_ => _.UserName == userName).ToListAsync();
        }
    }
}
