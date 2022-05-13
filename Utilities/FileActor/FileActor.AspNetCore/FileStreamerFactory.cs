using FileActor.Abstract;
using FileActor.FileServices;
using Microsoft.Extensions.DependencyInjection;
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

		public static explicit operator FileStreamerFactory(ServiceDescriptor v)
		{
			throw new NotImplementedException();
		}
	}
}