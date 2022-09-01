using FileActor.Abstract;
using Product.Application.DTOs;
using Product.Application.Exceptions;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;

namespace Product.Infrastructure.Services
{
	public class ProductService : GenericActiveService<Domain.Entities.Product, ProductDTO>, IProductService
	{
		private readonly IFileServiceActor _fileServiceProvider;
		public ProductService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceActor fileServiceProvider) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
		}
		public override void Add(ProductDTO entityDTO)
		{
			ArgumentNullException.ThrowIfNull(entityDTO);
			base.Add(entityDTO);
			if (!string.IsNullOrWhiteSpace(entityDTO.Id))
			{
				_fileServiceProvider.SaveAll(entityDTO);
			}
		}
		public override void Update(ProductDTO entityDTO)
		{
			ArgumentNullException.ThrowIfNull(entityDTO);
			var entityExists = string.IsNullOrWhiteSpace(entityDTO.Id) ? default : _repo.Exists(i=>i.Id==entityDTO.Id);
			if (entityExists)
			{
				_repo.Update(_mapper.Map<Domain.Entities.Product>(entityDTO));
				_fileServiceProvider.ReplaceAll(entityDTO);
				return;
			}
			throw new UpdateOperationException("entity is empty");
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
