using System;

namespace FileActor.Abstract.Factory
{
	/// <summary>
	/// factory for creation of related fileType helpers
	/// </summary>
	public interface IFileTypeHelperFactory
	{
		/// <summary>
		/// creates FileTypeHelper base on the given type
		/// </summary>
		/// <param name="type">typeof fileHelper</param>
		/// <returns>created FileTypeHelper</returns>
		IFileTypeHelper CreateFileHelper(Type type);
	}
}
