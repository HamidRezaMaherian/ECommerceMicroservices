using FileActor;

namespace Product.API.Configurations.DTOs;

public class ProductImageDTO : Application.DTOs.ProductImageDTO
{
	[FileAction(nameof(ImagePath), "/images")]
	public string Image { get; set; }
}
