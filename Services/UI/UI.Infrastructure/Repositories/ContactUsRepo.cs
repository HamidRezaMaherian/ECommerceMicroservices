using Services.Shared.Contracts;
using UI.Application.Repositories;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Repositories
{
	public class ContactUsRepo : Repository<ContactUs, ContactUsDAO>, IContactUsRepo
	{
		public ContactUsRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}
	}
}
