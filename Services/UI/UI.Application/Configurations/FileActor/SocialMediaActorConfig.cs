using FileActor.Internal;
using UI.Application.DTOs;

namespace UI.Application.Configurations;

public class SocialMediaFileActorConfig : FileActorConfigurable<SocialMediaDTO>
{
	public SocialMediaFileActorConfig()
	{
		StreamFor(i => i.ImagePath)
			.SetRelativePath("/images/socialMedias")
			.SetFileGet((obj) => obj.GetImage())
			.SetOnAfterSaved((obj, info) =>obj.ImagePath = info.ToString());
	}
}