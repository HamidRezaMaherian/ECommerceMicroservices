using FileActor;

namespace UI.API.Configurations.DTOs
{
	public class SliderDTO : Application.DTOs.SliderDTO
	{
		[FileAction(nameof(ImagePath), "/images")]
		public string Image { get; set; }
	}
}
