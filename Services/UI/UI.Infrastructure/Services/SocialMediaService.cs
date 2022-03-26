using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SocialMediaService : GenericActiveService<SocialMedia, SocialMediaDTO>, ISocialMediaService
	{
		public SocialMediaService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
