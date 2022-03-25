using AutoMapper;
using Discount.Application.UnitOfWork;
using Discount.Domain.Common;
using Services.Shared.AppUtils;
using Services.Shared.Common;
using Services.Shared.Contracts;
using System.Linq.Expressions;

namespace Discount.Application.Services
{
	public class DiscountBaseService: IDiscountBaseService
	{
		protected readonly ICustomMapper _mapper;
		protected readonly IUnitOfWork _unitOfWork;
		public DiscountBaseService(IUnitOfWork unitOfWork, ICustomMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public virtual DiscountBase GetById(object id)
		{
			return _unitOfWork.PercentDiscountRepo.Get(id) as DiscountBase ?? _unitOfWork.PriceDiscountRepo.Get(id);
		}
		public virtual TypeDTO GetById<TypeDTO>(object id) where TypeDTO : class
		{
			var modelDTO = _mapper.Map<TypeDTO>(GetById(id));
			return modelDTO;
		}
		public virtual IEnumerable<DiscountBase> GetAll()
		{
			return Enumerable.Concat<DiscountBase>(
				_unitOfWork.PercentDiscountRepo.Get(),
				_unitOfWork.PriceDiscountRepo.Get());
		}
		public virtual IEnumerable<TypeDTO> GetAll<TypeDTO>() where TypeDTO : class
		{
			return _mapper.Map<IEnumerable<TypeDTO>>(GetAll());
		}
		public virtual IEnumerable<DiscountBase> GetAll(Expression<Func<DiscountBase, bool>> condition)
		{
			return Enumerable.Concat<DiscountBase>(
				_unitOfWork.PercentDiscountRepo.Get(),
				_unitOfWork.PriceDiscountRepo.Get());
		}
		public virtual IEnumerable<TypeDTO> GetAll<TypeDTO>(Expression<Func<DiscountBase, bool>> condition) where TypeDTO : class
		{
			var ccc = GetAll(condition);
			return _mapper.Map<IEnumerable<TypeDTO>>(ccc);
		}

	}
}
