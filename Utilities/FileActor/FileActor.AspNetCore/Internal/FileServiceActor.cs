using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.AspNetCore.Internal
{
	public class FileServiceActor : IFileServiceActor
	{
		readonly IConfigurationManager _configurationManager;
		readonly IFileStreamerContainer _factory;
		readonly IFileTypeHelperProvider _fileTypeHelperProvider;
		private readonly IEnumerable<IFileStreamer> _streamers;

		public FileServiceActor(IConfigurationManager configurationManager,
			IFileStreamerContainer factory, IFileTypeHelperProvider fileTypeHelperProvider)
		{
			_configurationManager = configurationManager;
			_factory = factory;
			_fileTypeHelperProvider = fileTypeHelperProvider;
			_streamers = _factory.GetAll();
		}

		public void Delete<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			var _streamers = _factory.GetAll();
			Delete(obj, info, _streamers);
		}

		public void DeleteAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			Parallel.ForEach(allInfo, (info) =>
			{
				Delete(obj, info, _streamers);
			});
		}

		public async Task DeleteAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			 {
				 await DeleteAsync(obj, info, _streamers);
			 });
		}

		public async Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			await DeleteAsync(obj, info, _streamers);
		}

		public void Replace<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			if (info.Value != null)
			{
				Delete(obj, info, _streamers);
				Save(obj, info, _streamers);
			}
		}

		public void ReplaceAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			Parallel.ForEach(allInfo, (info) =>
			{
				if (info.Value != null)
				{
					Delete(obj, info, _streamers);
					Save(obj, info, _streamers);
				}
			});

		}

		public async Task ReplaceAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			{
				if (info.Value != null)
				{
					await DeleteAsync(obj, info, _streamers);
					await SaveAsync(obj, info, _streamers);
				}
			});
		}

		public async Task ReplaceAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			if (info.Value != null)
			{
				await DeleteAsync(obj, info, _streamers);
				await SaveAsync(obj, info, _streamers);
			}
		}

		public void Save<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			Save(obj, info, _streamers);
		}

		public void SaveAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			Parallel.ForEach(allInfo, (info) =>
			{
				Save(obj, info, _streamers);
			});
		}

		public async Task SaveAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj);
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			{
				await SaveAsync(obj, info, _streamers);
			});
		}

		public async Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			await SaveAsync(obj, info, _streamers);
		}

		#region PrivateMethods
		private void Save(object obj, FileStreamInfo info, IEnumerable<IFileStreamer> streamers)
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
		}
		private async Task SaveAsync(object obj, FileStreamInfo info, IEnumerable<IFileStreamer> streamers)
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
		}
		private void Delete(object obj, FileStreamInfo info, IEnumerable<IFileStreamer> streamers)
		{
			Parallel.ForEach(streamers, (streamer) =>
			{
				streamer.Delete(info.Value?.ToString());
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");

		}
		private async Task DeleteAsync(object obj, FileStreamInfo info, IEnumerable<IFileStreamer> streamers)
		{
			await Parallel.ForEachAsync(streamers, async (streamer, _) =>
			{
				await streamer.DeleteAsync(info.Value.ToString());
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, "");
		}
		#endregion
	}
}
