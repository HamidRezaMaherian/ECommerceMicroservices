﻿using Services.Shared.Common;

namespace Discount.Application.DTOs
{
	public class PercentDiscountDTO:EntityBase<string>
	{
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string ProductId { get; set; }
		public int Percent { get; set; }
	}
}
