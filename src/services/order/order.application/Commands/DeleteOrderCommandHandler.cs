using MediatR;
using order.application.Contracts.Persistence;
using order.application.Exceptions;
using order.domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace order.application.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete is null)
            {
                throw new NotFoundException(nameof(Order));
            }
            await _orderRepository.DeleteAsync(orderToDelete);

            return Unit.Value;
        }
    }
}
