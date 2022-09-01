using FileActor;
using System.Text.Json.Serialization;

namespace Product.API.Configurations.DTOs;

public class CreateProductImageDTO : Application.DTOs.ProductImageDTO
{
	[JsonIgnore]
	public override string Id { get => base.Id; set => base.Id = value; }
	public string Image { get; set; }
	[JsonIgnore]
	public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	public override object GetImage() => Image;
}
public class UpdateProductImageDTO : Application.DTOs.ProductImageDTO
{
	public string Image { get; set; }
	[JsonIgnore]
	public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	public override object GetImage()
	{
		return Image;
	}
}

