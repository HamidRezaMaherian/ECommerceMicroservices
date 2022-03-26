using Discount.Application.DTOs;
using Discount.Application.UnitOfWork;
using Discount.Domain.Entities;

namespace Discount.Application.Services
{
	public class PriceDiscountService : GenericActiveService<PriceDiscount, PriceDiscountDTO>,
		IPriceDiscountService
	{
		public PriceDiscountService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
