using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Application.Repositories;
using Product.Infrastructure.Persist;

namespace Product.Infrastructure.Repositories
{
	public abstract class Repository<T,TDAO> : IRepository<T>
		where T : class
		where TDAO : class
	{
		protected readonly ApplicationDbContext _db;
		protected readonly DbSet<TDAO> _dbSet;
		private readonly IMapper _mapper;
		public Repository(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_dbSet = _db.Set<TDAO>();
			_mapper = mapper;
		}
		public virtual void Add(T entity)
		{
			_dbSet.Add(_mapper.Map<TDAO>(entity));
		}
		public virtual void Delete(object id)
		{
			var entity = Get(id);
			if (entity == null)
				return;
			_dbSet.Remove(_mapper.Map<TDAO>(entity));
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(_mapper.Map<TDAO>(entity));
		}

		public virtual IQueryable<T> Get()
		{
			return _dbSet.Cast<T>();
		}

		public virtual T Get(object id)
		{
			return _mapper.Map<T>(_dbSet.Find(id));
		}

		public virtual void Update(T entity)
		{
			_dbSet.Update(entity as TDAO);
		}
	}
}
