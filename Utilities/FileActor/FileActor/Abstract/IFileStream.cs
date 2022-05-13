using System.Threading.Tasks;

namespace FileActor.Abstract
{
	public interface IFileStream<in Type>
	{
		void Upload(Type file, string path);
	}
	public interface IAsyncFileStream<in Type>
	{
		Task UploadAsync(Type file, string path);
	}
}

