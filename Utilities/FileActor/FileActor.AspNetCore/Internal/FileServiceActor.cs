using FileActor.Abstract;
using FileActor.Abstract.Factory;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.AspNetCore.Internal
{
	public class FileServiceActor : IFileServiceActor
	{
		readonly IConfigurationManager _configurationManager;
		readonly IFileStreamerContainer _factory;
		readonly IFileTypeHelperProvider _fileTypeHelperProvider;
		public FileServiceActor(IConfigurationManager configurationManager,
			IFileStreamerContainer factory, IFileTypeHelperProvider fileTypeHelperProvider)
		{
			_configurationManager = configurationManager;
			_factory = factory;
			_fileTypeHelperProvider = fileTypeHelperProvider;
		}

		public void Delete<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var streamers = _factory.GetAll();
			Parallel.ForEach(streamers, (streamer) =>
			{
				streamer.Delete(info.Value?.ToString());
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
		}

		public void DeleteAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			var streamers = _factory.GetAll();
			Parallel.ForEach(allInfo, (info) =>
			{
				if (info.Value != null)
				{
					foreach (var streamer in streamers)
					{
						streamer.Delete(info.Value.ToString());
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
				 if (info.Value != null)
				 {
					 await Parallel.ForEachAsync(streamers, async (streamer, _) =>
					 {
						 await streamer.DeleteAsync(info.Value.ToString());
					 });
				 }
				 obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
			 });
		}

		public async Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(streamers, async (streamer, _) =>
			 {
				 await streamer.DeleteAsync(info.Value?.ToString());
			 });
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
		}

		public void Save<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			var streamers = _factory.GetAll();
			if (info.Value != null)
			{
				var fileName = Guid.NewGuid().ToString();
				Parallel.ForEach(streamers, (streamer) =>
				{
					streamer.Upload(info.Value, info.RelativePath, fileName);
				});
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			}
		}

		public void SaveAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			var streamers = _factory.GetAll();
			Parallel.ForEach(allInfo, (info) =>
			{
				if (info.Value != null)
				{
					var fileName = Guid.NewGuid().ToString();
					foreach (var streamer in streamers)
					{
						streamer.Upload(info.Value, info.RelativePath, fileName);
					};
					var typeHelper = _fileTypeHelperProvider.ProvideFileHelper(info.Value.GetType());
					obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, $"{info.RelativePath}/{fileName}{typeHelper.GetExtension(info.Value)}");
				}
			});
		}

		public async Task SaveAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			var streamers = _factory.GetAll();
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			{
				if (info.Value != null)
				{
					var fileName = Guid.NewGuid().ToString();
					await Parallel.ForEachAsync(streamers, async (streamer, _) =>
					 {
						 await streamer.UploadAsync(info.Value, info.RelativePath, fileName);
					 });
					var typeHelper = _fileTypeHelperProvider.ProvideFileHelper(info.Value.GetType());
					obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, $"{info.RelativePath}/{fileName}{typeHelper.GetExtension(info.Value)}");
				}
			});
		}

		public async Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			var streamers = _factory.GetAll();
			if (info.Value != null)
			{
				await Parallel.ForEachAsync<IFileStreamer>(streamers, (async (streamer, _) =>
				{
					var fileName = Guid.NewGuid().ToString();
					await streamer.UploadAsync(info.Value, info.RelativePath, fileName);
					var typeHelper = _fileTypeHelperProvider.ProvideFileHelper(info.Value.GetType());
					obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, $"{info.RelativePath}/{fileName}{typeHelper.GetExtension(info.Value)}");
				}));
			}
		}
	}
}
