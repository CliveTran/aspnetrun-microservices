
using basket.api.Application.Interfaces;
using discount.grpc.Protos;

namespace basket.api.Grpc;
public class DiscountGrpcService : IDiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscountAsync(string productName)
    {
        var getDiscountRequest = new GetDiscountRequest { ProductName = productName };

        return await _discountProtoService.GetDiscountAsync(getDiscountRequest);
    }
}
