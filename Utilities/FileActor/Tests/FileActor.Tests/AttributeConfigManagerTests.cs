using FileActor.Internal;
using FileActor.Tests.Helpers;
using NUnit.Framework;
using System;

namespace FileActor.Tests
{
	[TestFixture]
	public class AttributeConfigurationManagerTests
	{
		private AttributeConfigManager _configManager;
		[SetUp]
		public void SetUp()
		{
			_configManager = new AttributeConfigManager();
		}
		[Test]
		[TestCase(typeof(FakeClass))]
		[TestCase(typeof(FakeClass1))]
		public void GetAllInfo_ReturnAllInfos(Type type)
		{
			//var validInfos = type.GetProperties()
			//	.Where(i => i.CustomAttributes.Any(a => a.AttributeType == typeof(FileActionAttribute)));
			//var result = (IEnumerable<FileStreamInfo>)_configManager.GetType().GetMethod("GetAllInfo")?.MakeGenericMethod(type).Invoke(_configManager, null);

			//Assert.AreEqual(
			//	validInfos.Count(),
			//	result?.Count());

			//CollectionAssert.AreEquivalent(validInfos.Select(i => i.Name), result?.Select(i => i.TargetProperty));
			Assert.Fail();
		}
		[Test]
		public void GetInfo_InfoExist_ReturnInfo()
		{
			//var validInfo = typeof(FakeClass).GetProperties()
			//	.FirstOrDefault(i => i.CustomAttributes.Any(a => a.AttributeType == typeof(FileActionAttribute)) && i.Name == "File");

			//Expression<Func<FakeClass, string>> exp = (i) => i.File;
			//var result = _configManager.GetInfo();

			//Assert.AreEqual(validInfo?.Name, result.PropertyName);
			Assert.Fail();
		}
		[Test]
		public void GetInfo_InfoNotExist_ThrowException()
		{
			//Expression<Func<FakeClass, int>> exp = (i) => i.MyProperty;
			//Assert.Throws<NotFoundException>(() =>
			//{
			//	_configManager.GetInfo(exp);
			//});
			Assert.Fail();
		}
	}
}