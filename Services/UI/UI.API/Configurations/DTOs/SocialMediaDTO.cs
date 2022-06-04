using FileActor;
using System.Text.Json.Serialization;

namespace UI.API.Configurations.DTOs
{
	public class SocialMediaDTO : Application.DTOs.SocialMediaDTO
	{
		[FileAction(nameof(ImagePath), "/images")]
		public string Image { get; set; }
		[JsonIgnore]
		public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	}
}
