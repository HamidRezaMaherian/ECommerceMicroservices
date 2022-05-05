using FileActor.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
namespace FileActor.Internal
{
	public class AttributeConfigManager : IConfigurationManager
	{
		public IEnumerable<FileStreamInfo> GetAllInfo<T>()
		{
			return typeof(T).GetType().GetProperties()
						 .Where(i => IsPropertyValid(i))
						 .Select(i =>
						 {
							 var attr = i.GetCustomAttribute<FileActionAttribute>();
							 return new FileStreamInfo(i.Name)
							 {
								 RelativePath = attr?.Path,
								 TargetProperty = attr?.TargetPropertyName
							 };
						 });
		}

		public FileStreamInfo GetInfo<T, TProperty>(Expression<Func<T, TProperty>> exp)
		{
			var member = exp.GetMember();
			var attr = typeof(T).GetProperty(member.Name)?.GetCustomAttribute<FileActionAttribute>();
			return new FileStreamInfo(member.Name)
			{
				RelativePath = attr?.Path,
				TargetProperty = attr?.TargetPropertyName
			};
		}

		private bool IsPropertyValid(PropertyInfo info)
		{
			return info.CustomAttributes.Any(a => a.AttributeType == typeof(FileActionAttribute));
		}
	}
	public class ObjectConfigManager : IConfigurationManager
	{
		private readonly IServiceProvider _serviceProvider;

		public ObjectConfigManager(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public IEnumerable<FileStreamInfo> GetAllInfo<T>()
		{
			var configurableObj = (FileActorConfigurable<T>)_serviceProvider.GetService(typeof(FileActorConfigurable<T>));
			return configurableObj.GetInfo();
		}

		public FileStreamInfo GetInfo<T, TProperty>(Expression<Func<T, TProperty>> exp)
		{
			var configurableObj = (FileActorConfigurable<T>)_serviceProvider.GetService(typeof(FileActorConfigurable<T>));
			return configurableObj.GetInfo(exp);
		}
	}
}
