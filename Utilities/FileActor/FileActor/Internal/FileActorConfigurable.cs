using FileActor.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Internal
{
	public abstract class FileActorConfigurable<T>
	{
		private IDictionary<string, FileStreamInfo> _configurations = new Dictionary<string, FileStreamInfo>();
		protected FileStreamInfo StreamFor<TProperty>(Expression<Func<T, TProperty>> exp)
		{
			var propertyName = exp.GetMember().Name;
			var info = new FileStreamInfo(propertyName);
			_configurations.TryAdd(propertyName, info);
			return info;
		}
		public FileStreamInfo GetInfo<TProperty>(Expression<Func<T, TProperty>> exp)
		{
			return _configurations[exp.GetMember().Name] ?? default;
		}
		public IEnumerable<FileStreamInfo> GetInfo()
		{
			return _configurations.Values;
		}
	}
}
