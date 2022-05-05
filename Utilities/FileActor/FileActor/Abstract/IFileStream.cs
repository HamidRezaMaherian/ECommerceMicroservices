namespace FileActor.Abstract
{
	public interface IFileStream<in Type>
	{
		void Upload(Type file, string path);
	}
}
