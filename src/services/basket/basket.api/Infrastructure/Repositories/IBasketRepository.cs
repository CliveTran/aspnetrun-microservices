
using basket.api.Domain;

namespace basket.api.Infrastructure.Repositories;
public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasket(string userName);

    Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);

    Task DeleteBasket(string userName);
}
