using AutoMapper;
using Discount.Application.DTOs;
using Discount.Application.UnitOfWork;
using Discount.Domain.Entities;
using Services.Shared.Contracts;

namespace Discount.Application.Services
{
	public class PercentDiscountService : GenericActiveService<PercentDiscount, PercentDiscountDTO>,
		IPercentDiscountService
	{
		public PercentDiscountService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
