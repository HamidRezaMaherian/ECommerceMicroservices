using System;

namespace FileActor.Abstract.Factory
{
	public interface IFileExtensionFactory
	{
		IFileExtension CreateFileExtension(Type streamType);
	}
}
