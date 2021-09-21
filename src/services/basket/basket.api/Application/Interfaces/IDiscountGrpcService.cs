using discount.grpc.Protos;
using System.Threading.Tasks;

namespace basket.api.Application.Interfaces;
public interface IDiscountGrpcService
{
    Task<CouponModel> GetDiscountAsync(string productName);
}
