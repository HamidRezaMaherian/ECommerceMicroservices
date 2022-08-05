using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
namespace FileActor.Internal
{
	public class AttributeConfigManager : IConfigurationManager
	{
		public IEnumerable<ObjectStreamConfiguration> GetAllInfo<T>(T obj) 
		{
			return obj.GetType().GetProperties()
			 .Where(i => IsPropertyValid(i))
			 .Select(i =>
			 {
				 var attr = i.GetCustomAttribute<FileActionAttribute>();
				 return new ObjectStreamConfigProxy<T>(i?.Name)
					.SetFileGet((obj) => i?.GetValue(obj).ToString())
					.SetOnAfterSaved((obj, info) => i?.SetValue(obj, info.ToString()))
					.SetOnAfterDeleted((obj) => i?.SetValue(obj, ""));
			 });
		}

		public ObjectStreamConfiguration GetInfo<T, TProperty>(T obj, Expression<Func<T, TProperty>> exp) 
		{
			try
			{
				var member = exp.GetMember();
				var propertyInfo = typeof(T).GetProperty(member.Name);
				var attr = propertyInfo?.GetCustomAttribute<FileActionAttribute>();
				return new ObjectStreamConfigProxy<T>(propertyInfo?.Name)
					.SetFileGet((obj) => propertyInfo?.GetValue(obj).ToString())
					.SetOnAfterSaved((obj, info) => propertyInfo?.SetValue(obj, info.ToString()))
					.SetOnAfterDeleted((obj) => propertyInfo?.SetValue(obj, ""));

			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message, e.InnerException);
			}
		}

		public ObjectStreamConfiguration GetInfo<T>(T obj, string propertyName) 
		{
			try
			{
				var propertyInfo = typeof(T).GetProperty(propertyName);
				var attr = propertyInfo?.GetCustomAttribute<FileActionAttribute>();
				return new ObjectStreamConfigProxy<T>(propertyInfo?.Name)
					.SetFileGet((obj) => propertyInfo?.GetValue(obj).ToString())
					.SetOnAfterSaved((obj, info) => propertyInfo?.SetValue(obj, info.ToString()))
					.SetOnAfterDeleted((obj)=>propertyInfo?.SetValue(obj,""));
			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message, e.InnerException);
			}
		}

		private bool IsPropertyValid(PropertyInfo info)
		{
			return info.CustomAttributes.Any(a => a.AttributeType == typeof(FileActionAttribute));
		}
	}
	public class ObjectConfigManager : IConfigurationManager
	{
		private readonly IConfigProvider _configProvider;

		public ObjectConfigManager(IConfigProvider configProvider)
		{
			_configProvider = configProvider;
		}
		public IEnumerable<ObjectStreamConfiguration> GetAllInfo<T>(T obj) 
		{
			var configurableObj = _configProvider.ProvideConfiguration(obj.GetType());
			return configurableObj.GetInfo();
		}

		public ObjectStreamConfiguration GetInfo<T, TProperty>(T obj, Expression<Func<T, TProperty>> exp) 
		{
			try
			{
				var configurableObj = _configProvider.ProvideConfiguration(obj.GetType());
				return configurableObj.GetInfo(exp.GetMember().Name);
			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message);
			}
		}

		public ObjectStreamConfiguration GetInfo<T>(T obj, string propertyName) 
		{
			try
			{
				var configurableObj = _configProvider.ProvideConfiguration(obj.GetType());
				return configurableObj.GetInfo(propertyName);
			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message);
			}
		}
	}
}
