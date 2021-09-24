using MediatR;
using Microsoft.Extensions.Logging;
using order.application.Contracts.Infrastructure;
using order.application.Contracts.Repositories;
using order.application.Models;
using order.domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace order.application.Commands
{
    internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = MappingOrder(request);
            var newAddedOrder = await _orderRepository.AddAsync(order);
            await SendEmail(newAddedOrder);
            return newAddedOrder.Id;
        }

        private async Task SendEmail(Order newAddedOrder)
        {
            var email = new Email
            {
                To = "tranvinhnhan.tech.clone@gmail.com",
                Body = $"Order Id: {newAddedOrder.Id} has been created.",
                Subject = "Order created successfully."
            };
            try
            {
                await _emailService.SendMail(email);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to send email, Exception: {e.Message}");
            }
        }

        private Order MappingOrder(CheckoutOrderCommand request)
        {
            return new Order
            {
                UserName = request.UserName,
                TotalPrice = request.TotalPrice,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress,
                AddressLine = request.AddressLine,
                Country = request.Country,
                State = request.State,
                ZipCode = request.ZipCode,
                CardName = request.CardName,
                CardNumber = request.CardNumber,
                Expiration = request.Expiration,
                CVV = request.CVV,
                PaymentMethod = request.PaymentMethod
            };
        }
    }
}
