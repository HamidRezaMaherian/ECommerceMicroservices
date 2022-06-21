using FileActor.Abstract;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class BrandService : GenericActiveService<Domain.Entities.Brand, BrandDTO>, IBrandService
	{
		private readonly IFileServiceActor _fileServiceProvider;
		public BrandService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceActor fileServiceProvider) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
		}
		public override void Add(BrandDTO entityDTO)
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
