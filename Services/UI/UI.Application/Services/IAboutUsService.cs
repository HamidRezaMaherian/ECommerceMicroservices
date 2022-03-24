using System.Linq.Expressions;
using UI.Application.DTOs;
using UI.Domain.Entities;

namespace UI.Application.Services
{
	public interface IAboutUsService
	{
		AboutUs FirstOrDefault();
		TypeDTO FirstOrDefault<TypeDTO>() where TypeDTO : class;
		void Update(AboutUsDTO entityDTO);
		bool Exists(Expression<Func<AboutUs, bool>> condition);
	}
}
