using AutoMapper;
using Discount.Application.DTOs;
using Discount.Application.UnitOfWork;
using Discount.Domain.Entities;
using Services.Shared.AppUtils;
using Services.Shared.Common;
using Services.Shared.Contracts;
using System.Linq.Expressions;

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
