using FileActor.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using UI.Application.FileServices;

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