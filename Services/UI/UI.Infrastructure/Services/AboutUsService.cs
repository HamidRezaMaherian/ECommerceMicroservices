using Services.Shared.Contracts;
using System.Linq.Expressions;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class AboutUsService : IAboutUsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICustomMapper _mapper;
		public AboutUsService(IUnitOfWork unitOfWork, ICustomMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public bool Exists(Expression<Func<AboutUs, bool>> condition)
		{
			return _unitOfWork.AboutUsRepo.Exists(condition);
		}

		public AboutUs FirstOrDefault()
		{
			return _unitOfWork.AboutUsRepo.Get().FirstOrDefault();
		}

		public TypeDTO FirstOrDefault<TypeDTO>() where TypeDTO : class
		{
			return _mapper.Map<TypeDTO>(FirstOrDefault());
		}

		public void Update(AboutUsDTO entityDTO)
		{
			var aboutUs = _unitOfWork.AboutUsRepo.Get().FirstOrDefault();
			_mapper.Map(entityDTO, aboutUs);
			_unitOfWork.AboutUsRepo.Update(aboutUs);
		}
	}
}
