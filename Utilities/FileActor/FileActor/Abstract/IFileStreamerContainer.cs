using System.Collections.Generic;

namespace FileActor.Abstract
{
	public interface IFileStreamerContainer
	{
		void Insert(string name, IFileStreamer fileStreamer);
		IEnumerable<IFileStreamer> GetAll();
		IEnumerable<string> GetAllNames();
		IFileStreamer GetByName(string name);
	}
}
