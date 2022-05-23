﻿using FileActor.Abstract;
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
			Parallel.ForEach(_factory.GetAll(), (streamer) =>
			{
				streamer.Delete(info.RelativePath);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void DeleteAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				foreach (var streamer in _factory.GetAll())
				{
					streamer.Delete(info.RelativePath);
				};
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public async Task DeleteAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			 {
				 var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				 await Parallel.ForEachAsync(_factory.GetAll(), async (streamer, _) =>
				 {
					 await streamer.DeleteAsync(info.RelativePath);
				 });
				 obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			 });
		}

		public async Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			await Parallel.ForEachAsync(_factory.GetAll(), async (streamer, _) =>
			 {
				 await streamer.DeleteAsync(info.RelativePath);
			 });
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void Save<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			Parallel.ForEach(_factory.GetAll(), (streamer) =>
			{
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(obj);
				var fileName = Guid.NewGuid().ToString() + extension;
				streamer.Upload(propertyValue, info.RelativePath, fileName);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}

		public void SaveAll<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			Parallel.ForEach(allInfo, (info) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(obj);
				var fileName = Guid.NewGuid().ToString() + extension;
				foreach (var streamer in _factory.GetAll())
				{
					streamer.Upload(propertyValue, info.RelativePath, fileName);
				};
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public async Task SaveAllAsync<T>(T obj)
		{
			var allInfo = _configurationManager.GetAllInfo(obj?.GetType());
			await Parallel.ForEachAsync(allInfo, async (info, _) =>
			{
				var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(obj);
				var fileName = Guid.NewGuid().ToString() + extension;
				await Parallel.ForEachAsync(_factory.GetAll(), async (streamer, _) =>
				 {
					 await streamer.UploadAsync(propertyValue, info.RelativePath, fileName);
				 });
				obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
			});
		}

		public async Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj)
		{
			var info = _configurationManager.GetInfo(obj?.GetType(), exp.GetMember().Name);
			var propertyValue = obj?.GetType().GetProperty(info.PropertyName)?.GetValue(obj);
			await Parallel.ForEachAsync(_factory.GetAll(), async (streamer,_) =>
			{
				var extension = _fileExtensionFactory.CreateFileExtension(propertyValue?.GetType()).GetExtension(obj);
				var fileName = Guid.NewGuid().ToString() + extension;
				await streamer.UploadAsync(propertyValue, info.RelativePath, fileName);
			});
			obj?.GetType().GetProperty(info.TargetProperty)?.SetValue(obj, info.RelativePath);
		}
	}
}