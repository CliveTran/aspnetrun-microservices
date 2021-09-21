using MediatR;
using System.Collections.Generic;

namespace order.application.Features.Orders.Queries
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public string UserName { get; set; }
    }
}
