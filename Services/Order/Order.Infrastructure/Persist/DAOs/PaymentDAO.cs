using Order.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class PaymentDAO : EntityPrimaryBaseDAO<string>
	{
		[Required]
		public PaymentStatus Status { get; set; }
		[Required]
		public PaymentMethod Method { get; set; }

		public string TransactionId { get; set; }
		//public Transaction Transaction { get; set; }
	}
}