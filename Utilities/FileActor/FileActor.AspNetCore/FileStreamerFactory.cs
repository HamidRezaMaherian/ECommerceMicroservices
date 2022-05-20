using FileActor.Abstract;
using FileActor.FileServices;
using System;

namespace FileActor.AspNetCore
{
	public class FileStreamerFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public FileStreamerFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public IFileStreamer CreateLocalFileStream(string rootPath)
		{
			return new LocalFileStreamer(rootPath, _serviceProvider);
		}
	}
}