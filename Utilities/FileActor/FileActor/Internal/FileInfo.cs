using System.IO;

namespace FileActor.Internal
{
	public class FileInfo
	{
		public FileInfo(string server, string relativePath, string name)
		{
			Server = server;
			RelativePath = relativePath;
			Name = name;
		}

		public string Server { get; set; }
		public string RelativePath { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Path.Combine(Server, RelativePath, Name);
		}
	}
}
