using Product.Domain.Common;
using Product.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
	public class Property : EntityBase<string>
	{
		//[MaxLength(300)]
		//[Required]
		public string Name { get; set; }
		public PropertyType Type { get; set; }
	}
}
