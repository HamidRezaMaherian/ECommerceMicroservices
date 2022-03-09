using AutoMapper;
using Discount.Application.Services;
using Discount.Domain.Common;
using DiscountRPC;
using Grpc.Core;

namespace Discount.API.Services
{
	public class DiscountRPCService : DiscountRPC.DiscountRPCService.DiscountRPCServiceBase
	{
		private readonly IDiscountBaseService _discountBaseService;

		public DiscountRPCService(IDiscountBaseService discountBaseService)
		{
			_discountBaseService = discountBaseService;
		}
		public override async Task GetDiscounts(ProductId request, IServerStreamWriter<DiscountBaseResult> responseStream, ServerCallContext context)
		{
			var res = _discountBaseService.GetAll<DiscountBaseResult>(i => i.ProductId == request.ProductId_);
			foreach (var item in res)
			{
				await responseStream.WriteAsync(item);
			}
		}
	}
}