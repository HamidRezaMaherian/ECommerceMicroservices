using System;

namespace FileActor
{
	public class FileActionAttribute : Attribute
	{
		public string Path { get; set; }
		public string FileProperty { get; set; }
		public FileActionAttribute(string fileProperty, string path)
		{
			FileProperty = fileProperty;
			Path = path;
		}
	}
}
