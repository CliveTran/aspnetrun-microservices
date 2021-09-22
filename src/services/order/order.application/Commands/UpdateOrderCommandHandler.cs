using MediatR;
using Microsoft.Extensions.Logging;
using order.application.Contracts.Persistence;
using order.application.Exceptions;
using order.domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace order.application.Commands
{
    internal class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                throw new NotFoundException(nameof(Order));
            }

            UpdateOrderDetail(request, orderToUpdate);
            await _orderRepository.UpdateAsync(orderToUpdate);
            return Unit.Value;
        }

        private static void UpdateOrderDetail(UpdateOrderCommand request, Order orderToUpdate)
        {
            orderToUpdate.UserName = request.UserName;
            orderToUpdate.TotalPrice = request.TotalPrice;
            orderToUpdate.FirstName = request.FirstName;
            orderToUpdate.LastName = request.LastName;
            orderToUpdate.EmailAddress = request.EmailAddress;
            orderToUpdate.AddressLine = request.AddressLine;
            orderToUpdate.Country = request.Country;
            orderToUpdate.State = request.State;
            orderToUpdate.ZipCode = request.ZipCode;
            orderToUpdate.CardName = request.CardName;
            orderToUpdate.CardNumber = request.CardNumber;
            orderToUpdate.Expiration = request.Expiration;
            orderToUpdate.CVV = request.CVV;
            orderToUpdate.PaymentMethod = request.PaymentMethod;
        }
    }
}
