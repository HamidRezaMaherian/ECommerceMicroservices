using System.Text.Json.Serialization;

namespace UI.API.Configurations.DTOs
{
	public class CreateSliderDTO : Application.DTOs.SliderDTO
	{
		[JsonIgnore]
		public override string Id { get => base.Id; set => base.Id = value; }
		public string Image { get; set; }
		[JsonIgnore]
		public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
		public override object GetImage() => Image;
	}
	public class UpdateSliderDTO : Application.DTOs.SliderDTO
	{
		public string Image { get; set; }
		[JsonIgnore]
		public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
		public override object GetImage()
		{
			return Image;
		}
	}
}
