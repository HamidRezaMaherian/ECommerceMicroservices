using Discount.Domain.Common;
using System.Linq.Expressions;

namespace Discount.Application.Services;

public interface IDiscountBaseService
{
	IEnumerable<DiscountBase> GetAll();
	IEnumerable<TypeDTO> GetAll<TypeDTO>() where TypeDTO : class;
	IEnumerable<DiscountBase> GetAll(Expression<Func<DiscountBase, bool>> condition);
	IEnumerable<TypeDTO> GetAll<TypeDTO>(Expression<Func<DiscountBase, bool>> condition) where TypeDTO : class;

	DiscountBase GetById(object id);
	TypeDTO GetById<TypeDTO>(object id) where TypeDTO : class;

}
