using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Abstract
{
	public interface IConfigurationManager
	{
		IEnumerable<FileStreamInfo> GetAllInfo<T>();
		FileStreamInfo GetInfo<T, TProperty>(Expression<Func<T, TProperty>> exp);
	}
}
