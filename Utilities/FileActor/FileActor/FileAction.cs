using System;

namespace FileActor
{
	public class FileActionAttribute : Attribute
	{
		public string Path { get; set; }
		public string TargetPropertyName { get; set; }
		public FileActionAttribute(string targetProperty, string path)
		{
			TargetPropertyName = targetProperty;
			Path = path;
		}
	}
}
