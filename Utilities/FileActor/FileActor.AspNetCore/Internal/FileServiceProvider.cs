using FileActor.Abstract;
using FileActor.Abstract.Factory;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.AspNetCore.Internal
{
	public class FileServiceProvider : IFileServiceProvider
	{
		readonly IConfigurationManager _configurationManager;
		readonly IFileStreamerContainer _factory;
		readonly IFileTypeHelperFactory _fileTypeHelperFactory;
		public FileServiceProvider(IConfigurationManager configurationManager,
			IFileStreamerContainer factory, IFileTypeHelperFactory fileTypeHelperFactory)
		{
			_configurationManager = configurationManager;
			_factory = factory;
			_fileTypeHelperFactory = fileTypeHelperFactory;
		}

		public void Delete<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			Parallel.ForEach(streamers, (streamer) =>
			{
				streamer.Delete(obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj)?.ToString());
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
		}

		public void DeleteAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				if (propertyValue != null)
				{
					foreach (var streamer in streamers)
					{
						streamer.Delete(propertyValue.ToString());
					};
				}
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
			});
		}

		public async Task DeleteAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			 {
				 var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				 if (propertyValue != null)
				 {
					 await Parallel.ForEachAsync(streamers, async (streamer, _) =>
					 {
						 await streamer.DeleteAsync(propertyValue.ToString());
					 });
				 }
				 obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
			 });
		}

		public async Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(streamers, async (streamer, _) =>
			 {
				 await streamer.DeleteAsync(obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj)?.ToString());
			 });
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
		}

		public void Save<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			if (propertyValue != null)
			{
				var fileName = Guid.NewGuid().ToString();
				Parallel.ForEach(streamers, (streamer) =>
				{
					streamer.Upload(propertyValue, info.RelativePath, fileName);
				});
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			}
		}

		public void SaveAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				if (propertyValue != null)
				{
					var fileName = Guid.NewGuid().ToString();
					foreach (var streamer in streamers)
					{
						streamer.Upload(propertyValue, info.RelativePath, fileName);
					};
					var typeHelper = _fileTypeHelperFactory.CreateFileHelper(propertyValue.GetType());
					obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, $"{info.RelativePath}/{fileName}{typeHelper.GetExtension(propertyValue)}");
				}
			});
		}

		public async Task SaveAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				if (propertyValue != null)
				{
					var fileName = Guid.NewGuid().ToString();
					await Parallel.ForEachAsync(streamers, async (streamer, _) =>
					 {
						 await streamer.UploadAsync(propertyValue, info.RelativePath, fileName);
					 });
					var typeHelper = _fileTypeHelperFactory.CreateFileHelper(propertyValue.GetType());
					obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, $"{info.RelativePath}/{fileName}{typeHelper.GetExtension(propertyValue)}");
				}
			});
		}

		public async Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			if (propertyValue != null)
			{
				await Parallel.ForEachAsync(streamers, async (streamer, _) =>
				{
					var fileName = Guid.NewGuid().ToString();
					await streamer.UploadAsync(propertyValue, info.RelativePath, fileName);
					var typeHelper = _fileTypeHelperFactory.CreateFileHelper(propertyValue.GetType());
					obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, $"{info.RelativePath}/{fileName}{typeHelper.GetExtension(propertyValue)}");
				});
			}
		}
	}
}
