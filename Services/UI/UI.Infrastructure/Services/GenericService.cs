using Services.Shared.AppUtils;
using System.Linq.Expressions;
using UI.Application.DTOs;
using UI.Application.Repositories;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Common;

namespace UI.Infrastructure.Services
{
	public abstract class GenericBaseService<Tid,T, Tdto> : IBaseService<T, Tdto> 
		where T : EntityPrimaryBase<Tid>
		where Tdto : BaseDTO<Tid>
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

		public virtual T FirstOrDefault()
		{
			return _repo.Get().FirstOrDefault();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _repo.Get().ToList();
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
			return _mapper.Map<IEnumerable<TypeDTO>>(GetAll(condition));
		}

		public virtual void Add(Tdto entityDTO)
		{
			var entity = _mapper.Map<T>(entityDTO);
			_repo.Add(entity);
			entityDTO.Id = entity.Id;
		}
		public virtual void Update(Tdto entityDTO)
		{
			var entity = _repo.Get(entityDTO.Id);
		   _mapper.Map(entityDTO, entity);
			_repo.Update(entity);
		}

		public virtual void Delete(object id)
		{
			_repo.Delete(id);
		}

		public bool Exists(Expression<Func<T, bool>> condition)
		{
			return _repo.Get().Any(condition.Compile());
		}

	}
	public abstract class GenericActiveService<Tid,T, Tdto> : GenericBaseService<Tid,T, Tdto>, IBaseActiveService<T, Tdto>
		where T : EntityPrimaryBase<Tid>, IBaseActive
		where Tdto : BaseDTO<Tid>
	{
		public GenericActiveService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper) { }

		public virtual IEnumerable<T> GetAllActive()
		{
			return
				_repo.Get().Where(i => i.IsActive).ToList()
				;
		}
		public virtual IEnumerable<T> GetAllActive(Expression<Func<T, bool>> condition)
		{
			return _repo.Get(new QueryParams<T>()
			{
				Expression = ExpressionHelper.And(i => i.IsActive, condition)
			});
		}
	}
}
