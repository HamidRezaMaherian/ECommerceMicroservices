using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.Internal;
using System;
using System.Collections.Generic;
using System.IO;
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


		public void Replace<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			var value = info.GetFileObj.Invoke(obj);
			if (value!= null)
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
				var value = info.GetFileObj.Invoke(obj);
				if (value!= null)
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
				var value = info.GetFileObj.Invoke(obj);
				if (value != null)
				{
					await DeleteAsync(obj, info, _streamers);
					await SaveAsync(obj, info, _streamers);
				}
			});
		}

		public async Task ReplaceAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj, exp.GetMember().Name);
			var value = info.GetFileObj.Invoke(obj);
			if (value!= null)
			{
				await DeleteAsync(obj, info, _streamers);
				await SaveAsync(obj, info, _streamers);
			}
		}
		#region PrivateMethods
		private void Save(object obj, ObjectStreamConfiguration info, IEnumerable<IFileStreamer> streamers)
		{
			var value = info.GetFileObj.Invoke(obj);
			if (value != null)
			{
				var fileName = Guid.NewGuid().ToString();
				foreach (var streamer in streamers)
				{
					streamer.Upload(value, info.RelativePath, fileName);
				};
				var typeHelper = _fileTypeHelperProvider.ProvideFileHelper(value.GetType());
				info.OnAfterSaved.Invoke(obj, new FileActor.Internal.FileInfo("", info.RelativePath, $"{fileName}{typeHelper.GetExtension(value)}"));
			}
		}
		private async Task SaveAsync(object obj, ObjectStreamConfiguration info, IEnumerable<IFileStreamer> streamers)
		{
			var value = info.GetFileObj.Invoke(obj);
			if (value != null)
			{
				var fileName = Guid.NewGuid().ToString();
				await Parallel.ForEachAsync(streamers, async (streamer, _) =>
				{
					await streamer.UploadAsync(value, info.RelativePath, fileName);
				});
				var typeHelper = _fileTypeHelperProvider.ProvideFileHelper(value.GetType());
				info.OnAfterSaved.Invoke(obj, new FileActor.Internal.FileInfo("", info.RelativePath, $"{fileName}{typeHelper.GetExtension(value)}"));
			}
		}
		private void Delete(object obj, ObjectStreamConfiguration info, IEnumerable<IFileStreamer> streamers)
		{
			Parallel.ForEach(streamers, (streamer) =>
			{
				streamer.Delete(
					Path.Combine(info.RelativePath, info.GetFileName.Invoke(obj))
					);
			});
			info.OnAfterDeleted.Invoke(obj);
		}
		private async Task DeleteAsync(object obj, ObjectStreamConfiguration info, IEnumerable<IFileStreamer> streamers)
		{
			await Parallel.ForEachAsync(streamers, async (streamer, _) =>
			{
				await streamer.DeleteAsync(
					Path.Combine(info.RelativePath, info.GetFileName.Invoke(obj))
					);
			});
			info.OnAfterDeleted.Invoke(obj);
		}
		#endregion
	}
}
