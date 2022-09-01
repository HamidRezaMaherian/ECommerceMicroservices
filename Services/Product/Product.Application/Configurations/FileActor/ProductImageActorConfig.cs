using FileActor.Internal;
using Product.Application.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Configurations;

public class ProductImageDTOFileActorConfig : FileActorConfigurable<ProductImageDTO>
{
	public ProductImageDTOFileActorConfig()
	{
		StreamFor(i => i.ImagePath)
			.SetRelativePath("/images/productImages")
			.SetFileGet((obj) => obj.GetImage())
			.SetOnAfterSaved((obj, info) => obj.ImagePath = info.ToString());
	}
}
public class ProductImageFileActorConfig : FileActorConfigurable<ProductImage>
{
	public ProductImageFileActorConfig()
	{
		StreamFor(i => i.Image)
			.SetRelativePath("/images/productImages")
			.SetGetFileName(obj => obj.Image.Name)
			.SetOnAfterDeleted(obj =>
			{
				obj.Image = null;
			});

	}
}