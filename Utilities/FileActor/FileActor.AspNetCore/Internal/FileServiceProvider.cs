using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.AspNetCore.Abstract;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.AspNetCore.Internal
{
	public class FileServiceProvider : IFileServiceProvider
	{
		readonly IConfigurationManager _configurationManager;
		readonly IFileStreamerContainer _factory;
		readonly IFileExtensionFactory _fileExtensionFactory;

		public FileServiceProvider(IConfigurationManager configurationManager,
			IFileStreamerContainer factory,
			IFileExtensionFactory fileExtensionFactory)
		{
			_configurationManager = configurationManager;
			_factory = factory;
			_fileExtensionFactory = fileExtensionFactory;
		}

		public void Delete<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			Parallel.ForEach(streamers, (streamer) =>
			{
				streamer.Delete(info.RelativePath);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void DeleteAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				foreach (var streamer in streamers)
				{
					streamer.Delete(info.RelativePath);
				};
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public async Task DeleteAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			 {
				 var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				 await Parallel.ForEachAsync(streamers, async (streamer, _) =>
				 {
					 await streamer.DeleteAsync(info.RelativePath);
				 });
				 obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			 });
		}

		public async Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(streamers, async (streamer, _) =>
			 {
				 await streamer.DeleteAsync(info.RelativePath);
			 });
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void Save<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			Parallel.ForEach(streamers, (streamer) =>
			{
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(propertyValue);
				var fileName = Guid.NewGuid().ToString() + extension;
				streamer.Upload(propertyValue, info.RelativePath, fileName);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void SaveAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(propertyValue);
				var fileName = Guid.NewGuid().ToString() + extension;
				foreach (var streamer in streamers)
				{
					streamer.Upload(propertyValue, info.RelativePath, fileName);
				};
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public async Task SaveAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(propertyValue);
				var fileName = Guid.NewGuid().ToString() + extension;
				await Parallel.ForEachAsync(streamers, async (streamer, _) =>
				 {
					 await streamer.UploadAsync(propertyValue, info.RelativePath, fileName);
				 });
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public async Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			await Parallel.ForEachAsync(streamers, async (streamer,_) =>
			{
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(propertyValue);
				var fileName = Guid.NewGuid().ToString() + extension;
				await streamer.UploadAsync(propertyValue, info.RelativePath, fileName);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}
	}
}
