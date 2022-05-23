using FileActor.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Abstract
{
	public interface IConfigurationManager
	{
		IEnumerable<FileStreamInfo> GetAllInfo<T>();
		IEnumerable<FileStreamInfo> GetAllInfo(Type type);
		FileStreamInfo GetInfo<T, TProperty>(Expression<Func<T, TProperty>> exp);
		FileStreamInfo GetInfo(Type type, string propertyName);
	}
}
