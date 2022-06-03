using Admin.Application.Models.UI;

namespace Admin.Application.Services.UI
{
	public interface ISocialMediaService : IQueryBaseService<SocialMedia>, ICommandBaseService<SocialMedia, SocialMedia>
	{
	}
}
