using discount.api.Domain.Entities;
using System.Threading.Tasks;

namespace discount.api.Infrastructure.Repositories;
public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);

    Task<bool> CreateDiscount(Coupon coupon);

    Task<bool> UpdateDiscount(Coupon coupon);

    Task<bool> DeleteDiscount(string productName);
}
