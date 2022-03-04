﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Utils
{
	public class QueryParams<T> where T : class
	{
		public Expression<Func<T, bool>> Expression { get; set; }
		public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
		//public string IncludeProperties { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
	}
}
