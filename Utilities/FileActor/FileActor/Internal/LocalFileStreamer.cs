using FileActor.Abstract;
using FileActor.Abstract.Factory;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileActor.FileServices
{
	public class LocalFileStreamer : IFileStreamer
	{
		private readonly string _rootPath;
		private readonly IFileStreamFactory _streamFactory;

		public LocalFileStreamer(string rootPath, IFileStreamFactory streamFactory)
		{
			_rootPath = rootPath;
			_streamFactory = streamFactory;
		}
		public void Delete(string path)
		{
			string filePath = Path.Combine(_rootPath, path);
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		public Task DeleteAsync(string path)
		{
			return Task.Run(() =>
			{
				string filePath = Path.Combine(_rootPath, path);
				if (File.Exists(filePath))
					File.Delete(filePath);
			});
		}

		public void Upload(object file, string path, string fileName)
		{
			IFileStream fileStream = _streamFactory.CreateFileStream(file.GetType());
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			fileStream?.Upload(file.ToString(), Path.Combine(_rootPath, path,fileName));
		}

		public async Task UploadAsync(object file, string path, string fileName)
		{
			IFileStream fileStream = _streamFactory.CreateFileStream(file.GetType());
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			if (fileStream != null)
				await fileStream.UploadAsync(file, Path.Combine(_rootPath, path,fileName));
		}
	}
}
