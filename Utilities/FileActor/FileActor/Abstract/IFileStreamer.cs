using System.Threading.Tasks;

namespace FileActor.Abstract
{
	/// <summary>
	/// abstraction for local or remote fileStreamers
	/// </summary>
	public interface IFileStreamer
	{
		/// <summary>
		/// uploads file with given path info
		/// </summary>
		/// <param name="file">file object</param>
		/// <param name="path">relative path</param>
		/// <param name="fileName">file name for saving file</param>
		void Upload(object file, string path,string fileName);
		/// <summary>
		/// deletes file with given path
		/// </summary>
		/// <param name="path">relative path with filename</param>
		void Delete(string path);
		/// <summary>
		/// deletes file with given path asynchronously
		/// </summary>
		/// <param name="path">relative path with filename</param>
		Task DeleteAsync(string path);
		/// <summary>
		/// uploads file with given path info asynchronously
		/// </summary>
		/// <param name="file">file object</param>
		/// <param name="path">relative path</param>
		/// <param name="fileName">file name for saving file</param>
		Task UploadAsync(object file, string path, string fileName);
	}
}
