using System;
using System.Linq.Expressions;

namespace FileActor.Internal
{
	public class FileStreamInfo
	{
		public FileStreamInfo(object value, string relativePath, string targetProperty)
		{
			Value = value;
			RelativePath = relativePath;
			TargetProperty = targetProperty;
		}

		public object Value { get; private set; }
		public string RelativePath { get; private set; }
		public string TargetProperty { get; private set; }
	}
}