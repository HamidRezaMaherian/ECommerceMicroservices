using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
