using FileActor.Abstract;
using FileActor.Abstract.Factory;
using System.IO;
using System.Threading.Tasks;

namespace FileActor.FileServices
{
	public class LocalFileStreamer : IFileStreamer
	{
		private readonly string _rootPath;
		private readonly IFileTypeHelperFactory _fileTypeFactory;

		public LocalFileStreamer(string rootPath, IFileTypeHelperFactory streamFactory)
		{
			_rootPath = rootPath;
			_fileTypeFactory = streamFactory;
		}
		public void Delete(string path)
		{
			string filePath = $"{_rootPath}/{path}";
			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		public Task DeleteAsync(string path)
		{
			return Task.Run(() =>
			{
				string filePath = Path.Combine($"{_rootPath}/{path}");
				if (File.Exists(filePath))
					File.Delete(filePath);
			});
		}

		public void Upload(object file, string path, string fileName)
		{
			IFileTypeHelper fileHelper = _fileTypeFactory.CreateFileHelper(file.GetType());
			var fullPath = $"{_rootPath}/{ path}";
			if (!Directory.Exists(fullPath))
				Directory.CreateDirectory(fullPath);
			fileHelper?.Upload(file.ToString(), $"{fullPath}/{fileName}{fileHelper.GetExtension(file)}");
		}

		public async Task UploadAsync(object file, string path, string fileName)
		{
			IFileTypeHelper fileHelper = _fileTypeFactory.CreateFileHelper(file.GetType());
			var fullPath = $"{_rootPath}/{ path}";
			if (!Directory.Exists(fullPath))
				Directory.CreateDirectory(fullPath);
			if (fileHelper != null)
				await fileHelper.UploadAsync(file, $"{fullPath}/{fileName}{fileHelper.GetExtension(file)}");
		}
	}
}
