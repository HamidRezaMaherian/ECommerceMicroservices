using FileActor.Abstract;
using System.Collections.Generic;

namespace FileActor.Internal
{
	public class InMemoryFileStreamerContainer : IFileStreamerContainer
	{
		private readonly IDictionary<string, IFileStreamer> _fileStreams;
		public InMemoryFileStreamerContainer()
		{
			_fileStreams = new Dictionary<string, IFileStreamer>();
		}

		public IEnumerable<IFileStreamer> GetAll()
		{
			return _fileStreams.Values;
		}

		public IEnumerable<string> GetAllNames()
		{
			return _fileStreams.Keys;
		}

		public IFileStreamer GetByName(string name)
		{
			return _fileStreams[name] ?? default;
		}

		public void Insert(string name, IFileStreamer fileStreamer)
		{
			_fileStreams.TryAdd(name, fileStreamer);
		}
	}
}
