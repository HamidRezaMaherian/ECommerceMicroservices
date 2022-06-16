using FileActor;
using System.Text.Json.Serialization;

namespace UI.API.Configurations.DTOs
{
	public class CreateSliderDTO : Application.DTOs.SliderDTO
	{
		[JsonIgnore]
		public override string Id { get => base.Id; set => base.Id = value; }
		[FileAction(nameof(ImagePath), "/images")]
		public string Image { get; set; }
		[JsonIgnore]
		public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	}
	public class UpdateSliderDTO : Application.DTOs.SliderDTO
	{
		[FileAction(nameof(ImagePath), "/images")]
		public string Image { get; set; }
		[JsonIgnore]
		public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	}
}
