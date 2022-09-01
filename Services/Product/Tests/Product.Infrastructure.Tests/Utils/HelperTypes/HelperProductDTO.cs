using Product.Application.DTOs;

namespace Product.Infrastructure.Tests.Utils.HelperTypes;

public class HelperProductDTO : ProductDTO
{
	public override object GetMainImage()
	{
		return null;
	}
}
