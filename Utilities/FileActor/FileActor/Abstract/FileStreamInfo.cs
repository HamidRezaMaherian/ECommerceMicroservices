namespace FileActor.Abstract
{
	public class FileStreamInfo
	{
		public FileStreamInfo(string propertyName)
		{
			PropertyName = propertyName;
		}
		public string PropertyName { get; set; }
		public string RelativePath { get; set; }
		public string TargetProperty { get; set; }
	}
}