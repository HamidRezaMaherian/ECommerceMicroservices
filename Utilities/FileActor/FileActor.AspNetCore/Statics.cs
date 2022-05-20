using FileActor.Abstract;
using FileActor.Internal;

namespace FileActor.AspNetCore
{
	public static class Statics
	{
		private static IFileStreamerContainer _fileStreamerContainer;
		public static IFileStreamerContainer FileStreamerContainer
		{
			get
			{
				_fileStreamerContainer ??= new InMemoryFileStreamerContainer();
				return _fileStreamerContainer;
			}
		}
	}
}
