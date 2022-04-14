using Order.Domain.Common;

namespace Order.Application.DTOs
{
	public class OrderDTO : EntityBase<string>
	{
		public string UserName { get; set; }
	}
}
