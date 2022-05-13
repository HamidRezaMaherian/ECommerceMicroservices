using System.Threading.Tasks;

namespace FileActor.Abstract
{
	public interface IFileStreamer
	{
		void Upload(object file, string path);
		void Delete(string path);
		Task DeleteAsync(string path);
		Task UploadAsync(object file, string path);
	}
}
