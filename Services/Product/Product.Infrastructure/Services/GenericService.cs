using Product.Application.Repositories;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;
using Product.Domain.Common;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Product.Infrastructure.Services
{
	public abstract class GenericBaseService<T, Tdto> : IBaseService<T, Tdto> where T : class
	{
		protected readonly IRepository<T> _repo;
		protected readonly ICustomMapper _mapper;
		protected readonly IUnitOfWork _unitOfWork;
		public GenericBaseService(IUnitOfWork unitOfWork, ICustomMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_repo = unitOfWork.GetRepo<T>();
		}

		public virtual T GetById(object id)
		{
			return _repo.Get(id);
		}
		public virtual TypeDTO GetById<TypeDTO>(object id) where TypeDTO : class
		{
			var modelDTO = _mapper.Map<TypeDTO>(GetById(id));
			return modelDTO;
		}

		public virtual T FirstOrDefault()
		{
			return _repo.Get().FirstOrDefault();
		}
		public virtual TypeDTO FirstOrDefault<TypeDTO>() where TypeDTO : class
		{
			return _mapper.Map<TypeDTO>(FirstOrDefault());
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _repo.Get().ToList();
		}
		public virtual IEnumerable<TypeDTO> GetAll<TypeDTO>() where TypeDTO : class
		{
			return _mapper.Map<IEnumerable<TypeDTO>>(GetAll());
		}
		public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> condition)
		{
			return _repo.Get(new QueryParams<T>()
			{
				Expression = condition
			}).ToList();
		}
		public virtual IEnumerable<TypeDTO> GetAll<TypeDTO>(Expression<Func<T, bool>> condition) where TypeDTO : class
		{
			var ccc = GetAll(condition);
			return _mapper.Map<IEnumerable<TypeDTO>>(ccc);
		}

		public virtual void Add(Tdto entityDTO)
		{
			var entity = _mapper.Map<T>(entityDTO);
			_repo.Add(entity);
			_mapper.Map(entity, entityDTO);
		}
		public virtual void Update(Tdto entityDTO)
		{
			var entity = _mapper.Map<T>(entityDTO);
			_repo.Update(entity);
			_unitOfWork.Save();
		}

		public virtual void Delete(object id)
		{
			_repo.Delete(id);
			_unitOfWork.Save();
		}

		public bool Exists(Expression<Func<T, bool>> condition)
		{
			return _repo.Get().Any(condition.Compile());
		}
	}
	public abstract class GenericActiveService<T, Tdto> : GenericBaseService<T, Tdto>, IBaseActiveService<T, Tdto> where T : class, IBaseActive
	{
		public GenericActiveService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper) { }

		public virtual IEnumerable<T> GetAllActive()
		{
			return
				_repo.Get().Where(i => i.IsActive).ToList()
				;
		}
		public virtual IEnumerable<TypeDTO> GetAllActive<TypeDTO>() where TypeDTO : class
		{
			return _mapper.Map<IEnumerable<TypeDTO>>(GetAllActive());
		}
		public virtual IEnumerable<T> GetAllActive(Expression<Func<T, bool>> condition)
		{
			return _repo.Get(new QueryParams<T>()
			{
				Expression = ExpressionHelper.And(i => i.IsActive, condition)
			});
		}
		public virtual IEnumerable<TypeDTO> GetAllActive<TypeDTO>(Expression<Func<T, bool>> condition) where TypeDTO : class
		{
			return _mapper.Map<IEnumerable<TypeDTO>>(GetAllActive(condition));
		}
	}
}
