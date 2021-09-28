
using basket.api.Domain;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace basket.api.Infrastructure.Repositories;
public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redis;
    public BasketRepository(IDistributedCache redis)
    {
        _redis = redis;
    }

    public async Task DeleteBasket(string userName)
    {
        await _redis.RemoveAsync(userName);
    }

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var jsonString = await _redis.GetStringAsync(userName);
        return string.IsNullOrEmpty(jsonString) ? null : JsonSerializer.Deserialize<ShoppingCart>(jsonString);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        await _redis.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));

        return await GetBasket(basket.UserName);
    }
}
