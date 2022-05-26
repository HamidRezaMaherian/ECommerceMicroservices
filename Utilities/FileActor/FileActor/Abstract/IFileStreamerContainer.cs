using System.Collections.Generic;

namespace FileActor.Abstract
{
	/// <summary>
	/// Abstraction of container for registered fileStreamers like inMemory or database configurations
	/// </summary>
	public interface IFileStreamerContainer
	{
		void Insert(string name, IFileStreamer fileStreamer);
		IEnumerable<IFileStreamer> GetAll();
		IEnumerable<string> GetAllNames();
		IFileStreamer GetByName(string name);
	}
}
