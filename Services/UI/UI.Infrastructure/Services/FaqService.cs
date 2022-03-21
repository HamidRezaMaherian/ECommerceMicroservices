using Services.Shared.Contracts;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class FaqService : GenericActiveService<FAQ, FaqDTO>, IFaqService
	{
		public FaqService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
