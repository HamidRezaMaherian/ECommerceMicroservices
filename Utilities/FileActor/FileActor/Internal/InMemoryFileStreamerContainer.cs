using FileActor.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FileActor.Internal
{
	public class InMemoryFileStreamerContainer : IFileStreamerContainer
	{
		private readonly IDictionary<string, Expression<Func<IFileStreamer>>> _fileStreams;
		public InMemoryFileStreamerContainer()
		{
			_fileStreams = new Dictionary<string, Expression<Func<IFileStreamer>>>();
		}

		public IEnumerable<IFileStreamer> GetAll()
		{
			return _fileStreams.Values.Select(i=>i.Compile().Invoke());
		}

		public IEnumerable<string> GetAllNames()
		{
			return _fileStreams.Keys;
		}

		public IFileStreamer GetByName(string name)
		{
			return _fileStreams[name].Compile().Invoke() ?? default;
		}

		public void Insert(string name, IFileStreamer fileStreamer)
		{
			Expression<Func<IFileStreamer>> factor = () =>  fileStreamer ;
			_fileStreams.TryAdd(name, factor);
		}
	}
}
