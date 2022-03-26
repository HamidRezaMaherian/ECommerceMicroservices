using System.Linq.Expressions;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class ContactUsService : IContactUsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICustomMapper _mapper;
		public ContactUsService(IUnitOfWork unitOfWork, ICustomMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public bool Exists(Expression<Func<ContactUs, bool>> condition)
		{
			return _unitOfWork.ContactUsRepo.Exists(condition);
		}

		public ContactUs FirstOrDefault()
		{
			return _unitOfWork.ContactUsRepo.Get().FirstOrDefault();
		}

		public TypeDTO FirstOrDefault<TypeDTO>() where TypeDTO : class
		{
			return _mapper.Map<TypeDTO>(FirstOrDefault());
		}

		public void Update(ContactUsDTO entityDTO)
		{
			var aboutUs = _unitOfWork.ContactUsRepo.Get().FirstOrDefault();
			_mapper.Map(entityDTO, aboutUs);
			_unitOfWork.ContactUsRepo.Update(aboutUs);
		}
	}
}
