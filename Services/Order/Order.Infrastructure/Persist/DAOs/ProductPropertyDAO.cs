﻿using System.ComponentModel.DataAnnotations;

namespace Order.Infrastructure.Persist.DAOs
{
	public class ProductPropertyDAO : EntityBaseDAO<string>
	{
		[Required]
		[MaxLength(500)]
		public string Value { get; set; }
		[Required]
		public string PropertyId { get; set; }
		[Required]
		public string ProductId { get; set; }

		#region NavigationProps
		public virtual ProductDAO Product { get; set; }
		public virtual PropertyDAO Property { get; set; }
		#endregion
	}
}