using System;

namespace FileActor.Abstract.Factory
{
	/// <summary>
	/// provider for related fileType helpers
	/// </summary>
	public interface IFileTypeHelperProvider
	{
		/// <summary>
		/// provides FileTypeHelper base on the given type
		/// </summary>
		/// <param name="type">typeof fileHelper</param>
		/// <returns>created FileTypeHelper</returns>
		IFileTypeHelper ProvideFileHelper(Type type);
	}
}
