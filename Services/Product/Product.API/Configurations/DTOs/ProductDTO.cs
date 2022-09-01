using FileActor;
using System.Text.Json.Serialization;

namespace Product.API.Configurations.DTOs;

public class CreateProductDTO : Application.DTOs.ProductDTO
{
	[JsonIgnore]
	public override string Id { get => base.Id; set => base.Id = value; }
	public string MainImage { get; set; }
	[JsonIgnore]
	public override string MainImagePath { get => base.MainImagePath; set => base.MainImagePath = value; }
	public override object GetMainImage() => MainImage;
}
public class UpdateProductDTO : Application.DTOs.ProductDTO
{
	public string MainImage { get; set; }
	[JsonIgnore]
	public override string MainImagePath { get => base.MainImagePath; set => base.MainImagePath = value; }
	public override object GetMainImage()
	{
		return MainImage;
	}
}
