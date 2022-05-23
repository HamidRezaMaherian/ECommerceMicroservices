using System;

namespace FileActor.Abstract.Factory
{
	public interface IFileStreamFactory
	{
		IFileStream CreateFileStream(Type streamType);
	}
}
