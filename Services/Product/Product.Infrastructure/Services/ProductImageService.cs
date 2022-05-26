using FileActor.Abstract;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductImageService : GenericActiveService<Domain.Entities.ProductImage, ProductImageDTO>, IProductImageService
	{
		private readonly IFileServiceProvider _fileServiceProvider;
		public ProductImageService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceProvider fileServiceProvider) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
		}
		public override void Add(ProductImageDTO entityDTO)
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
