using MediatR;
using order.application.Models;
using System.Collections.Generic;

namespace order.application.Queries
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public string UserName { get; set; }
    }
}
