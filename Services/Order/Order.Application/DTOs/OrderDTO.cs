using Order.Domain.Common;

namespace Order.Domain.Entities
{
	public class OrderDTO : EntityBase<string>
	{
		public string UserName { get; set; }
	}
}
