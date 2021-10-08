using basket.api.Application.Interfaces;
using basket.api.Application.Models;
using basket.api.Domain;
using basket.api.Infrastructure.Repositories;
using EventBus.Message.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace basket.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IDiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(
            IBasketRepository repository,
            IDiscountGrpcService discountGrpcService,
            IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new(userName));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")] // equal [HttpPost("Checkout")]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfo info)
        {
            var basket = await _repository.GetBasket(info.UserName);
            if (basket is null)
                return BadRequest();

            var eventMessage = Mapping(info, basket.TotalPrice);

            BasketCheckoutEvent Mapping(CheckoutInfo info, decimal totalPrice)
            {
                return new BasketCheckoutEvent
                {
                    UserName = info.UserName,
                    TotalPrice = totalPrice,
                    FirstName = info.FirstName,
                    LastName = info.LastName,
                    EmailAddress = info.EmailAddress,
                    AddressLine = info.AddressLine,
                    Country = info.Country,
                    State = info.State,
                    ZipCode = info.ZipCode,
                    CardName = info.CardName,
                    CardNumber = info.CardNumber,
                    Expiration = info.Expiration,
                    CVV = info.CVV,
                    PaymentMethod = info.PaymentMethod
                };
            }

            await _publishEndpoint.Publish(eventMessage);

            await _repository.DeleteBasket(info.UserName);

            return Accepted();
        }
    }
}
