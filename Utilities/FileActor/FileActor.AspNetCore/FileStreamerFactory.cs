using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.FileServices;
using System;

namespace FileActor.AspNetCore
{
	public class FileStreamerFactory
	{
		private readonly IFileStreamFactory _streamFactory;

		public FileStreamerFactory(IFileStreamFactory streamFactory)
		{
			_streamFactory = streamFactory;
		}
		public IFileStreamer CreateLocalFileStream(string rootPath)
		{
			return new LocalFileStreamer(rootPath, _streamFactory);
		}
	}
}