using FileActor;

namespace UI.API.Configurations.DTOs
{
	public class SocialMediaDTO : Application.DTOs.SocialMediaDTO
	{
		[FileAction(nameof(ImagePath), "/images")]
		public string Image { get; set; }
	}
}
