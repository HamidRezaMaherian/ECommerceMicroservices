using FileActor.Internal;
using Moq;
using System;

namespace FileActor.Tests.Helpers
{
	public static class Extensions
	{
		public static IServiceProvider MockServiceProvider()
		{
			var mock = new Mock<IServiceProvider>();
			mock.Setup(x => x.GetService(typeof(FileActorConfigurable<FakeClass>))).Returns(new FakeClassConfiguration());
			mock.Setup(x => x.GetService(typeof(FileActorConfigurable<FakeClass1>))).Returns(new FakeClass1Configuration());
			return mock.Object;
		}
	}
	public class FakeClass1
	{
		public string File { get; set; }
		public string FilePath { get; set; }
	}
	public class FakeClass
	{
		[FileAction(nameof(FilePath), "/test")]
		public string File { get; set; }
		public string FilePath { get; set; }
		public int MyProperty { get; set; }
	}
	class FakeClassConfiguration : FileActorConfigurable<FakeClass>
	{
		public FakeClassConfiguration()
		{
			StreamFor(i => i.File).SetPath("/test").SetTargetProperty("FilePath");
		}
	}
	class FakeClass1Configuration : FileActorConfigurable<FakeClass1>
	{
	}
}