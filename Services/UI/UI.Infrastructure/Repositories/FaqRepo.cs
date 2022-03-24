using MongoDB.Driver;
using Services.Shared.Contracts;
using UI.Application.Exceptions;
using UI.Application.Repositories;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Repositories
{
	public class FaqRepo : Repository<FAQ, FaqDAO>, IFaqRepo
	{
		public FaqRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}

		public override void Add(FAQ entity)
		{
			var faqCategoryDbSet = _db.DataBase.GetCollection<FaqCategoryDAO>(typeof(FaqCategory).Name);
			if (faqCategoryDbSet.AsQueryable().Any(i => i.Id == entity.CategoryId))
				base.Add(entity);
			else
				throw new InsertOperationException("The given {storeId} not exists");
		}
	}
}
