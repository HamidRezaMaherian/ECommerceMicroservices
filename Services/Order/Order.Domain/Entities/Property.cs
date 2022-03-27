using Order.Domain.Common;

namespace Order.Domain.Entities
{
	public class Property : EntityPrimaryBase<string>
	{
		public string Name { get; set; }
	}
}
