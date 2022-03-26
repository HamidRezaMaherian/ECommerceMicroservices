using Product.Domain.Common;
using Product.Domain.Enums;

namespace Product.Domain.Entities
{
	public class Property : EntityPrimaryBase<string>
	{
		public string Name { get; set; }
		public PropertyType Type { get; set; }
	}
}
