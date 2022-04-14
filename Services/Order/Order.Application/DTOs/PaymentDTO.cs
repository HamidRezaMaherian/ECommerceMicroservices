using Order.Domain.Common;
using Order.Domain.Enums;

namespace Order.Application.DTOs
{
	public class PaymentDTO : EntityPrimaryBase<string>
	{
		public PaymentStatus Status { get; set; }
		public PaymentMethod Method { get; set; }
		public string TransactionId { get; set; }
	}
}