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
		private readonly IObjectMocker<Slider, SliderDTO> _objectMocker;
		public SliderService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceActor fileServiceProvider, IObjectMocker<Slider, SliderDTO> objectMocker) : base(unitOfWork, mapper)
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

			base.Update(entityDTO);
		}
		public override void Delete(object id)
		{
			var obj = _repo.Get(id);
			if (obj != null)
			{
				_fileServiceProvider.DeleteAll(_objectMocker.MockObject(obj));
				_repo.Delete(id);
			}
		}
	}
}
