﻿using basket.api.Domain;
using basket.api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace basket.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
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
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
