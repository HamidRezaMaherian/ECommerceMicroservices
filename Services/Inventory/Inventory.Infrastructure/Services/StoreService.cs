using FileActor.Abstract;
using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Application.Tools;
using Inventory.Application.UnitOfWork;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure.Services
{
	public class StoreService : GenericActiveService<Store, StoreDTO>, IStoreService
	{
		private readonly IFileServiceProvider _fileServiceProvider;
		public StoreService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
		public override void Add(StoreDTO entityDTO)
		{
			_fileServiceProvider.SaveAll(entityDTO);
			base.Add(entityDTO);
		}
		public override void Delete(object id)
		{
			var obj = _repo.Get(id);
			if (obj != null)
			{
				_fileServiceProvider.DeleteAll(obj);
				_repo.Delete(id);
			}
		}

	}
}
