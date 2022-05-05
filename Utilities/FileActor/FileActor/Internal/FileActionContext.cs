
using FileActor.Abstract;
using System.Collections.Generic;

namespace FileActor.Internal
{
	public abstract class FileActionContext<T> where T : class
	{
		private readonly HashSet<string> _validStreamers = new HashSet<string>();

		protected void SetStreamer(string name)
		{
			_validStreamers.Add(name);
		}
	}
	internal class GlobalFileActionContext : FileActionContext<object>
	{
		public GlobalFileActionContext(IFileStreamerContainer _factory)
		{
			foreach (var item in _factory.GetAllNames())
			{
				SetStreamer(item);
			}
		}
	}
}
