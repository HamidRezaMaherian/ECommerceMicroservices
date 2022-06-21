using FileActor.Abstract;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductService : GenericActiveService<Domain.Entities.Product, ProductDTO>, IProductService
	{
		private readonly IFileServiceActor _fileServiceProvider;
		public ProductService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
		public override void Add(ProductDTO entityDTO)
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
