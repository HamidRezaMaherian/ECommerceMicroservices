using FileActor.Internal;
using Product.Application.DTOs;
using Product.Domain.Entities;

namespace Product.Application.Configurations;

public class BrandDTOFileActorConfig : FileActorConfigurable<BrandDTO>
{
	public BrandDTOFileActorConfig()
	{
		StreamFor(i => i.ImagePath)
			.SetRelativePath("/images/brands")
			.SetFileGet((obj) => obj.GetImage())
			.SetOnAfterSaved((obj, info) => obj.ImagePath = info.ToString());
	}
}
public class BrandFileActorConfig : FileActorConfigurable<Brand>
{
	public BrandFileActorConfig()
	{
		StreamFor(i => i.Image)
			.SetRelativePath("/images/brands")
			.SetGetFileName(obj => obj.Image.Name)
			.SetOnAfterDeleted(obj =>
			{
				obj.Image = null;
			});

	}
}