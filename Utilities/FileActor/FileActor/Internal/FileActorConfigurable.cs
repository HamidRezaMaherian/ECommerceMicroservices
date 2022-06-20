using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Internal
{
	public abstract class FileActorConfigurable<T> : IFileActorConfigurable
	{
		private IDictionary<string, ObjectStreamConfigProxy> _configurations = new ConcurrentDictionary<string, ObjectStreamConfigProxy>();
		protected ObjectStreamConfigProxy StreamFor<TProperty>(Expression<Func<T, TProperty>> exp)
		{
			var propertyName = exp.GetMember().Name;
			var info = new ObjectStreamConfigProxy(propertyName);
			_configurations.TryAdd(propertyName, info);
			return info;
		}
		public ObjectStreamConfiguration GetInfo<TProperty>(Expression<Func<T, TProperty>> exp)
		{
			return _configurations[exp.GetMember().Name] ?? default;
		}
		public IEnumerable<ObjectStreamConfiguration> GetInfo()
		{
			return _configurations.Values;
		}

		public ObjectStreamConfiguration GetInfo(string property)
		{
			return _configurations[property] ?? default;
		}
	}

	public interface IFileActorConfigurable
	{
		public ObjectStreamConfiguration GetInfo(string property);
		public IEnumerable<ObjectStreamConfiguration> GetInfo();
	}
}
