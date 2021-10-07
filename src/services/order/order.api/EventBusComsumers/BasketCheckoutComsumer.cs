using EventBus.Message.Events;
using MassTransit;
using MediatR;
using order.application.Commands;
using System.Threading.Tasks;

namespace order.api.EventBusComsumers
{
    public class BasketCheckoutComsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;

        public BasketCheckoutComsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            CheckoutOrderCommand Mapping(BasketCheckoutEvent @event)
            {
                return new CheckoutOrderCommand
                {
                    UserName = @event.UserName,
                    TotalPrice = @event.TotalPrice,
                    FirstName = @event.FirstName,
                    LastName = @event.LastName,
                    EmailAddress = @event.EmailAddress,
                    AddressLine = @event.AddressLine,
                    Country = @event.Country,
                    State = @event.State,
                    ZipCode = @event.ZipCode,
                    CardName = @event.CardName,
                    CardNumber = @event.CardNumber,
                    Expiration = @event.Expiration,
                    CVV = @event.CVV,
                    PaymentMethod = @event.PaymentMethod
                };
            }
            var checkoutCommand = Mapping(context.Message);

            await _mediator.Send(checkoutCommand);
        }
    }
}
