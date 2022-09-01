using System.Text.Json.Serialization;

namespace Product.API.Configurations.DTOs;

public class CreateBrandDTO : Application.DTOs.BrandDTO
{
	[JsonIgnore]
	public override string Id { get => base.Id; set => base.Id = value; }
	public string Image { get; set; }
	[JsonIgnore]
	public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	public override object GetImage() => Image;
}
public class UpdateBrandDTO : Application.DTOs.BrandDTO
{
	public string Image { get; set; }
	[JsonIgnore]
	public override string ImagePath { get => base.ImagePath; set => base.ImagePath = value; }
	public override object GetImage()
	{
		return Image;
	}
}