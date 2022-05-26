using System;

namespace FileActor.Abstract.Factory
{
	public interface IFileTypeHelperFactory
	{
		IFileTypeHelper CreateFileHelper(Type streamType);
	}
}
