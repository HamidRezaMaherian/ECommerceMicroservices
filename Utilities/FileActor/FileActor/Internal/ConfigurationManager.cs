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
		public IEnumerable<FileStreamInfo> GetAllInfo<T>(T obj)
		{
			return obj.GetType().GetProperties()
			 .Where(i => IsPropertyValid(i))
			 .Select(i =>
			 {
				 var attr = i.GetCustomAttribute<FileActionAttribute>();
				 return new FileStreamInfo(i.GetValue(obj), attr?.Path, attr?.TargetPropertyName);
			 });
		}

		public FileStreamInfo GetInfo<T, TProperty>(T obj, Expression<Func<T, TProperty>> exp)
		{
			try
			{
				var member = exp.GetMember();
				var propertyInfo = typeof(T).GetProperty(member.Name);
				var attr = propertyInfo?.GetCustomAttribute<FileActionAttribute>();
				return new FileStreamInfo(propertyInfo.GetValue(obj), attr?.Path, attr?.TargetPropertyName);
			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message, e.InnerException);
			}
		}

		public FileStreamInfo GetInfo<T>(T obj, string propertyName)
		{
			try
			{
				var propertyInfo = typeof(T).GetProperty(propertyName);
				var attr = propertyInfo?.GetCustomAttribute<FileActionAttribute>();
				return new FileStreamInfo(propertyInfo.GetValue(obj), attr?.Path, attr?.TargetPropertyName);
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
		public IEnumerable<FileStreamInfo> GetAllInfo<T>(T obj)
		{
			var configurableObj = _configProvider.ProvideConfiguration(obj.GetType());
			return configurableObj.GetInfo().Select(i=>new FileStreamInfo(i.Expression.Compile().Invoke(obj),i.RelativePath,i.Target));
		}

		public FileStreamInfo GetInfo<T, TProperty>(T obj, Expression<Func<T, TProperty>> exp)
		{
			try
			{
				var configurableObj = _configProvider.ProvideConfiguration(obj.GetType());
				var config = configurableObj.GetInfo(exp.GetMember().Name);
				return new FileStreamInfo(config.Expression.Compile().Invoke(obj), config.RelativePath, config.Target);
			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message);
			}
		}

		public FileStreamInfo GetInfo<T>(T obj, string propertyName)
		{
			try
			{
				var configurableObj = _configProvider.ProvideConfiguration(obj.GetType());
				var config = configurableObj.GetInfo(propertyName);
				return new FileStreamInfo(config.Expression.Compile().Invoke(obj), config.RelativePath, config.Target);
			}
			catch (Exception e)
			{
				throw new NotFoundException(e.Message);
			}
		}
	}
}
