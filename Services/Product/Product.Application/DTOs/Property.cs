using Product.Domain.Enums;
using Services.Shared.Common;

namespace Product.Application.DTOs;
public class PropertyDTO : EntityBase<string>
{
	public virtual string Name { get; set; }
	public PropertyType Type { get; set; }
}
