using FileActor.Internal;
using System;

namespace FileActor
{
	public class FileActionAttribute : Attribute
	{
		public string Path { get; set; }
		public string FileProperty { get; set; }
		public Action<object, FileInfo> OnAfterAction { get; private set; }
		public FileActionAttribute(string fileProperty, string path)
		{
			FileProperty = fileProperty;
			Path = path;
			OnAfterAction = (obj, info) => obj.GetType().GetProperty(fileProperty).SetValue(obj,
				string.Concat(info.Server, info.RelativePath, info.Name));
		}
	}
}
