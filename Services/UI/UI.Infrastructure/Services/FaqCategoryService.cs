using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class FaqCategoryService : GenericActiveService<string,FaqCategory, FaqCategoryDTO>, IFaqCategoryService
	{
		public FaqCategoryService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
