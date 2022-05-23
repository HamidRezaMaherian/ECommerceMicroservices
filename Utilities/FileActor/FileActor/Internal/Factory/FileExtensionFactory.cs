using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Internal.Factory
{
	public class FileExtensionFactory : IFileExtensionFactory
	{
		private readonly IDictionary<string, Expression<Func<IFileExtension>>> _fileStreams;
		public FileExtensionFactory()
		{
			_fileStreams = new ConcurrentDictionary<string, Expression<Func<IFileExtension>>>();
		}
		public void Add(Type type, Expression<Func<IFileExtension>> factor)
		{
			_fileStreams.Add(type.Name, factor);
		}

		public IFileExtension CreateFileExtension(Type streamType)
		{
			if (_fileStreams.TryGetValue(streamType.Name, out var stream))
			{
				return stream.Compile().Invoke();
			}
			throw new NotFoundException("no file stream found");
		}
	}
}
