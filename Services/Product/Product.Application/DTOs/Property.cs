using Product.Domain.Common;
using Product.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product.Application.DTOs;
	public class PropertyDTO : EntityBase<string>
{
	public virtual string Name { get; set; }
	public PropertyType Type { get; set; }
}
