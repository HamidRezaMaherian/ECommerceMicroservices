using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;

namespace UI.Infrastructure.Services
{
	public class SliderService : GenericActiveService<Slider, SliderDTO>, ISliderService
	{
		public SliderService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
