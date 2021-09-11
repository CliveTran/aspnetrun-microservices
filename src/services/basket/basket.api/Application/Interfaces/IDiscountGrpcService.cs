
using discount.grpc.Protos;

namespace basket.api.Application.Interfaces;
public interface IDiscountGrpcService
{
    Task<CouponModel> GetDiscountAsync(string productName);
}
