using FileActor;

namespace Product.API.Configurations.DTOs;

public class ProductDTO : Application.DTOs.ProductDTO
{
	[FileAction(nameof(MainImage), "/images")]
	public string MainImage { get; set; }
}
