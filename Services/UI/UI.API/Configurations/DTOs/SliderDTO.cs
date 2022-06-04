using FileActor;
using System.Text.Json.Serialization;

namespace UI.API.Configurations.DTOs
{
	public class SliderDTO : Application.DTOs.SliderDTO
	{
		[FileAction(nameof(ImagePath), "/images")]
		public string Image { get; set; }
		[JsonIgnore]
		public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	}
}
