using FileActor.Internal;
using UI.Application.DTOs;

namespace UI.Application.Configurations;

public class SliderFileActorConfig : FileActorConfigurable<SliderDTO>
{
	public SliderFileActorConfig()
	{
		StreamFor(i => i.ImagePath)
			.SetRelativePath("/images/sliders")
			.SetExpression((obj) => obj.GetImage());
	}
}