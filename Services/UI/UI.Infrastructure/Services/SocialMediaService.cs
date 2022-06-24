using FileActor.Abstract;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SocialMediaService : GenericActiveService<string,SocialMedia, SocialMediaDTO>, ISocialMediaService
	{
		private readonly IFileServiceActor _fileServiceProvider;
		private readonly IObjectMocker<SocialMedia,SocialMediaDTO> _objectMocker;
		public SocialMediaService(IUnitOfWork unitOfWork,
			ICustomMapper mapper,
			IFileServiceActor fileServiceProvider,
			IObjectMocker<SocialMedia, SocialMediaDTO> objectMocker) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
			_objectMocker = objectMocker;
		}
		public override void Add(SocialMediaDTO entityDTO)
		{
			_fileServiceProvider.SaveAll(entityDTO);
			base.Add(entityDTO);
		}
		public override void Delete(object id)
		{
			var obj = _repo.Get(id);
			if (obj != null)
			{
				_fileServiceProvider.DeleteAll(_objectMocker.MockObject(obj));
				_repo.Delete(obj);
			}
		}

	}
}
