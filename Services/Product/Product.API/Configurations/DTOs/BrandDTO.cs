using FileActor;

namespace Product.API.Configurations.DTOs;

public class BrandDTO:Application.DTOs.BrandDTO
{
	[FileAction(nameof(ImagePath),"/images")]
	public string Image { get; set; }
}