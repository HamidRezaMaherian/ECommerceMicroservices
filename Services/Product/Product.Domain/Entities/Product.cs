﻿using Product.Domain.Common;
using Product.Domain.ValueObjects;

namespace Product.Domain.Entities
{
	public class Product : EntityBase<string>
	{
		public string Name { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }
		public decimal UnitPrice { get; set; }

		public Blob MainImage { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public string CategoryId { get; set; }
		#region Relations
		public ProductCategory Category { get; set; }
		public IReadOnlyCollection<ProductImage> Images { get; set; }
		#endregion
	}
}
