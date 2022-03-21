using System.Linq.Expressions;
using UI.Application.DTOs;
using UI.Domain.Entities;

namespace UI.Application.Services
{
	public interface IContactUsService
	{
		ContactUs FirstOrDefault();
		TypeDTO FirstOrDefault<TypeDTO>() where TypeDTO : class;
		void Update(ContactUsDTO entityDTO);
		void Delete(object id);
		bool Exists(Expression<Func<ContactUs, bool>> condition);
	}
}
