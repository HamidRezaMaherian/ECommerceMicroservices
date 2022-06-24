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
		private readonly IObjectMocker _objectMocker;
		public SocialMediaService(IUnitOfWork unitOfWork,
			ICustomMapper mapper,
			IFileServiceActor fileServiceProvider,
			IObjectMocker objectMocker) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
			_objectMocker = objectMocker;
		}
			public override void Add(SocialMediaDTO entityDTO)
		{
			_fileServiceProvider.SaveAll(entityDTO);
			base.Add(entityDTO);
		}
		public override void Update(SocialMediaDTO entityDTO)
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
				var entityDTO = _objectMocker.MockObject<SocialMediaDTO>();
				entityDTO.SetFiles(obj);
				_fileServiceProvider.DeleteAll(entityDTO);
				_repo.Delete(id);
			}
		}
	}
}
