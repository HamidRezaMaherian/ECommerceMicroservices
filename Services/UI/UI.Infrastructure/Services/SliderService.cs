using FileActor.Abstract;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SliderService : GenericActiveService<string,Slider, SliderDTO>, ISliderService
	{
		private readonly IFileServiceActor _fileServiceProvider;
		private readonly IObjectMocker _objectMocker;
		public SliderService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceActor fileServiceProvider, IObjectMocker objectMocker) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
			_objectMocker = objectMocker;
		}
		public override void Add(SliderDTO entityDTO)
		{
			_fileServiceProvider.SaveAll(entityDTO);
			base.Add(entityDTO);
		}
		public override void Update(SliderDTO entityDTO)
		{
			var entity = _repo.Get(entityDTO.Id);
			entityDTO.SetFiles(entity);
			_fileServiceProvider.ReplaceAll(entityDTO);
			_mapper.Map(entityDTO, entity);
			_repo.Update(entity);
		}
		public override void Delete(object id)
		{
			var obj = _repo.Get(id);
			if (obj != null)
			{
				var entityDTO = _objectMocker.MockObject<SliderDTO>();
				entityDTO.SetFiles(obj);
				_fileServiceProvider.DeleteAll(entityDTO);
				_repo.Delete(id);
			}
		}
	}
}
