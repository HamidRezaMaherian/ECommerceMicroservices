using FileActor.Internal;
using Product.Application.DTOs;

namespace Product.Application.Configurations;

public class ProductDTOFileActorConfig : FileActorConfigurable<ProductDTO>
{
	public ProductDTOFileActorConfig()
	{
		StreamFor(i => i.MainImagePath)
			.SetRelativePath("/images/products")
			.SetFileGet(obj => obj.GetMainImage())
			.SetOnAfterSaved((obj, info) =>
			{
				obj.MainImagePath = info.ToString();
			});
	}
}

public class ProductFileActorConfig : FileActorConfigurable<Product.Domain.Entities.Product>
{
	public ProductFileActorConfig()
	{
		StreamFor(i => i.MainImage)
			.SetRelativePath("/images/products")
			.SetGetFileName(obj => obj.MainImage.Name)
			.SetOnAfterDeleted(obj =>
			{
				obj.MainImage = null;
			});

	}
}