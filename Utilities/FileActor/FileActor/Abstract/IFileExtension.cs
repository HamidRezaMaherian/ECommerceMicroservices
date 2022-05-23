namespace FileActor.Abstract
{
	public abstract class FileExtension<Type> : IFileExtension where Type : class
	{
		public abstract string GetExtension(Type file);
		public string GetExtension(object file)
		{
			return GetExtension((Type)file);
		}
	}
	public interface IFileExtension
	{
		public string GetExtension(object file);
	}

}