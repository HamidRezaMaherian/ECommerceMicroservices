using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.API.Configurations.DTOs;
using UI.API.Controllers;
using UI.API.Tests.Utils;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Domain.Entities;
using static UI.API.Tests.Utils.TestUtilsExtension;

namespace UI.API.Tests.Unit
{
	[TestFixture]
	public class FaqControllerTests
	{
		private IFaqService _faqService;
		private ICustomMapper _customMapper;
		private FaqController _faqController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _faqs = new List<FAQ>();
			_customMapper = CreateMapper(new TestMapperProfile());
			_faqService = MockAction<FAQ, FaqDTO>
				.MockServie<IFaqService>(_faqs).Object;
			_faqController = new FaqController(_faqService);
		}
		[Test]
		public void GetAll_ReturnAllUIs()
		{
			var faq = CreateFaq();
			var res = _faqController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _faqService.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_PasValidEntity_AddFaq()
		{
			var faq = new CreateFaqDTO()
			{
				CategoryId = Guid.NewGuid().ToString(),
				Question = "no q",
				Answer = "no answer",
				IsActive = true
			};
			_faqController.Create(faq);
			Assert.IsNotNull(_faqService.GetById(faq.Id));
		}
		[Test]
		public void Update_PasValidEntity_UpdateFaq()
		{
			var faq = _customMapper.Map<UpdateFaqDTO>(CreateFaq());
			faq.Question = "updatedQuestion";
			_faqController.Update(faq);

			Assert.AreEqual(_faqService.GetById(faq.Id).Question, faq.Question);
		}
		[Test]
		public void Delete_PasValidId_DeleteFaq()
		{
			var faq = CreateFaq();
			_faqController.Delete(faq.Id);

			Assert.IsFalse(_faqService.Exists(i => i.Id == faq.Id));
		}
		#region HelperMethods
		private FAQ CreateFaq()
		{
			var faq = new CreateFaqDTO()
			{
				CategoryId = Guid.NewGuid().ToString(),
				Question = "no q",
				Answer = "no answer",
				IsActive = true
			};
			_faqService.Add(faq);
			return _faqService.GetById(faq.Id);
		}
		#endregion
	}
}
