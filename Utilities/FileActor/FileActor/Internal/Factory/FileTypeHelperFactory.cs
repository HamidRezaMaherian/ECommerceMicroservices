using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Internal.Factory
{
	public class FileStreamFactory : IFileTypeHelperFactory
	{
		private readonly IDictionary<string, Expression<Func<IFileTypeHelper>>> _fileStreams;
		public FileStreamFactory()
		{
			_fileStreams = new ConcurrentDictionary<string, Expression<Func<IFileTypeHelper>>>();
		}
		public void Add(Type type, Expression<Func<IFileTypeHelper>> factor)
		{
			_fileStreams.Add(type.Name, factor);
		}
		public IFileTypeHelper CreateFileHelper(Type streamType)
		{
			if (_fileStreams.TryGetValue(streamType.Name, out var stream))
			{
				return stream.Compile().Invoke();
			}
			throw new NotFoundException("no file stream found");
		}
	}
}
