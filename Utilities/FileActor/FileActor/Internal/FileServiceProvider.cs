using FileActor.Abstract;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.Internal
{
	public class FileServiceProvider : IFileServiceProvider
	{
		readonly IConfigurationManager _configurationManager;
		readonly IFileStreamerContainer _factory;

		public FileServiceProvider(IConfigurationManager configurationManager, IFileStreamerContainer factory)
		{
			_configurationManager = configurationManager;
			_factory = factory;
		}

		public void Delete<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(exp);
			Parallel.ForEach(_factory.GetAll(), (streamer) =>
			{
				streamer.Delete(info.RelativePath);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void DeleteAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo<T>();
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				foreach (var streamer in _factory.GetAll())
				{
					streamer.Delete(info.RelativePath);
				};
				obj.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public void Save<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(exp);
			var propertyValue = obj.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			Parallel.ForEach(_factory.GetAll(), (streamer) =>
			{
				streamer.Upload(propertyValue, info.RelativePath);
			});
			obj.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void SaveAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo<T>();
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				foreach (var streamer in _factory.GetAll())
				{
					streamer.Upload(propertyValue, info.RelativePath);
				};
				obj.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}
	}
}
