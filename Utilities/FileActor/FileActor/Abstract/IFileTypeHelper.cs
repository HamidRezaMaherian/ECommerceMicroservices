using System.Threading.Tasks;

namespace FileActor.Abstract
{
	public abstract class FileTypeHelper<Type>:IFileTypeHelper where Type : class
	{
		public abstract void Upload(Type file, string path);
		public abstract Task UploadAsync(Type file, string path);
		public void Upload(object file, string path)
		{
			Upload((Type)file, path);
		}
		public Task UploadAsync(object file, string path)
		{
			return UploadAsync((Type)file, path);
		}
		public abstract string GetExtension(Type file);
		public string GetExtension(object file)
		{
			return GetExtension((Type)file);
		}
	}
	public interface IFileTypeHelper
	{
		public void Upload(object file, string path);
		public Task UploadAsync(object file, string path);
		public string GetExtension(object file);
	}
}

