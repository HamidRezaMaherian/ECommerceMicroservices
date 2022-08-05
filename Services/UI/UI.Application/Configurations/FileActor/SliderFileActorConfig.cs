using FileActor.Internal;
using UI.Application.DTOs;
using UI.Domain.Entities;

namespace UI.Application.Configurations;

public class SliderDTOFileActorConfig : FileActorConfigurable<SliderDTO>
{
	public SliderDTOFileActorConfig()
	{
		StreamFor(i => i.ImagePath)
			.SetRelativePath("/images/sliders")
			.SetFileGet(obj => obj.GetImage())
			.SetOnAfterSaved((obj, info) =>
			{
				obj.ImagePath = info.ToString();
			});
	}
}

public class SliderFileActorConfig : FileActorConfigurable<Slider>
{
	public SliderFileActorConfig()
	{
		StreamFor(i => i.Image)
			.SetRelativePath("/images/sliders")
			.SetFileGet(obj => obj.Image.Name)
			.SetGetFileName(obj=>obj.Image.Name)
			.SetOnAfterDeleted(obj =>
			{
				obj.Image = null;
			});

	}
}