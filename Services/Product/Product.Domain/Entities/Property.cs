using Product.Domain.Enums;
using Services.Shared.Common;

namespace Product.Domain.Entities
{
	public class Property : EntityPrimaryBase<string>
	{
		public string Name { get; set; }
		public PropertyType Type { get; set; }
	}
}
