using FileActor.AspNetCore.Abstract;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SliderService : GenericActiveService<Slider, SliderDTO>, ISliderService
	{
		private readonly IFileServiceProvider _fileServiceProvider;
		public SliderService(IUnitOfWork unitOfWork, ICustomMapper mapper, IFileServiceProvider fileServiceProvider) : base(unitOfWork, mapper)
		{
			_fileServiceProvider = fileServiceProvider;
		}
		public override void Add(SliderDTO entityDTO)
		{
			_fileServiceProvider.SaveAll(entityDTO);
			base.Add(entityDTO);
		}
	}
}
