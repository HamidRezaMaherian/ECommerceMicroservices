using FileActor.Abstract;
using System;
using System.IO;

namespace UI.Application.FileServices
{
	public class LocalFileStreamer : IFileStreamer
	{
		private readonly string _rootPath;
		private readonly IServiceProvider _serviceProvider;

		public LocalFileStreamer(string rootPath, IServiceProvider serviceProvider)
		{
			_rootPath = rootPath;
			_serviceProvider = serviceProvider;
		}
		public void Delete(string path)
		{
			string filePath = Path.Combine(_rootPath, path);
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		public void Upload(object file, string path)
		{
			IFileStream<object> fileStream = (IFileStream<object>)_serviceProvider.GetService(typeof(IFileStream<>).MakeGenericType(file.GetType()));
			fileStream?.Upload(file, Path.Combine(_rootPath, path));
		}
	}
}
