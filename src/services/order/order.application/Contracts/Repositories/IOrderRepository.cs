using order.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace order.application.Contracts.Repositories
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserNameAsync(string userName);
    }
}
