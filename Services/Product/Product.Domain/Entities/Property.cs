using Product.Domain.Common;
using Product.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
	public class Property : EntityBase<string>
	{
		public virtual string Name { get; set; }
		public PropertyType Type { get; set; }
	}
}
