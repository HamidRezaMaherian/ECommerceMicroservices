using FileActor.Abstract;
using FileActor.Exceptions;
using FileActor.Internal;
using FileActor.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FileActor.Tests
{
	[TestFixture]
	public class ObjectConfigurationManagerTests
	{
		private ObjectConfigManager _configManager;
		[SetUp]
		public void SetUp()
		{
			_configManager = new ObjectConfigManager(Helpers.Extensions.MockServiceProvider());
		}
		[Test]
		public void GetAllInfo_ReturnAllInfos()
		{
			var validInfos = new FakeClassConfiguration().GetInfo();

			var result = (IEnumerable<FileStreamInfo>)_configManager.GetType().GetMethod("GetAllInfo")?.MakeGenericMethod(typeof(FakeClass)).Invoke(_configManager, null);

			Assert.AreEqual(
				validInfos.Count(),
				result?.Count());

			CollectionAssert.AreEquivalent(validInfos.Select(i => i.PropertyName), result?.Select(i => i.PropertyName));
		}
		[Test]
		public void GetInfo_InfoExist_ReturnInfo()
		{
			var validInfo = (new FakeClassConfiguration()).GetInfo(i=>i.File);

			Expression<Func<FakeClass, string>> exp = (i) => i.File;
			var result = _configManager.GetInfo(exp);

			Assert.AreEqual(validInfo?.PropertyName, result.PropertyName);
		}
		[Test]
		public void GetInfo_InfoNotExist_ThrowException()
		{
			Expression<Func<FakeClass, int>> exp = (i) => i.MyProperty;
			Assert.Throws<NotFoundException>(() =>
			{
				_configManager.GetInfo(exp);
			});
		}
	}
}