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
			.SetExpression((obj) => obj.GetImage());
	}
}

public class SliderFileActorConfig : FileActorConfigurable<Slider>
{
	public SliderFileActorConfig()
	{
		StreamFor(i => i.Image)
			.SetRelativePath("/images/sliders")
			.SetExpression((obj) => null);

	}
}