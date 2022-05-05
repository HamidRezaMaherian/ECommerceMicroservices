namespace FileActor.Abstract
{
	public interface IFileStreamer
	{
		void Upload(object file, string path);
		void Delete(string path);
	}
}
