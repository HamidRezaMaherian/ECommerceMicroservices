using Services.Shared.Contracts;
using UI.Application.Repositories;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Repositories
{
	public class SocialMediaRepo : Repository<SocialMedia, SocialMediaDAO>, ISocialMediaRepo
	{
		public SocialMediaRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}
