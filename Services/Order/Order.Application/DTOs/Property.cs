using Order.Domain.Common;

namespace Order.Application.DTOs;
public class PropertyDTO : EntityBase<string>
{
	public virtual string Name { get; set; }
}
