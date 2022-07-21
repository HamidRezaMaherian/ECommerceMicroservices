using Admin.Application.DTOs.UI;
using Admin.Application.Models.UI;

namespace Admin.Application.Services.UI
{
	public interface ISliderService: IQueryBaseService<Slider>, ICommandBaseService<Slider, SliderDTO>
	{
	}
}
