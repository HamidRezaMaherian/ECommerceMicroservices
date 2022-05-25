using FileActor.Abstract;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SocialMediaService : GenericActiveService<SocialMedia, SocialMediaDTO>, ISocialMediaService
	{
		private readonly IFileServiceProvider _fileServiceProvider;
		public SocialMediaService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
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
				_fileServiceProvider.DeleteAll(obj);
				_repo.Delete(obj);
			}
		}

	}
}
