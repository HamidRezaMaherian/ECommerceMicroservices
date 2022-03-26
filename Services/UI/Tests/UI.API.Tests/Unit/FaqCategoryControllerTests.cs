using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UI.API.Controllers;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Domain.Entities;
using static UI.API.Tests.Utils.TestUtilsExtension;

namespace UI.API.Tests.Unit
{
	[TestFixture]
	public class FaqCategoryControllerTests
	{
		private IFaqCategoryService _faqCategoryService;
		private FaqCategoryController _faqCategoryController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _faqCategorys = new List<FaqCategory>();
			_faqCategoryService = MockAction<FaqCategory, FaqCategoryDTO>
				.MockServie<IFaqCategoryService>(_faqCategorys).Object;
			_faqCategoryController = new FaqCategoryController(_faqCategoryService);
		}
		[Test]
		public void GetAll_ReturnAllUIs()
		{
			var faqCategory = CreateFaqCategory();
			var res = _faqCategoryController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _faqCategoryService.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_PasValidEntity_AddFaqCategory()
		{
			var faqCategory = new FaqCategoryDTO()
			{
				Name = "no name",
				IsActive = true
			};
			_faqCategoryController.Create(faqCategory);
			Assert.IsNotNull(_faqCategoryService.GetById(faqCategory.Id));
		}
		[Test]
		public void Update_PasValidEntity_UpdateFaqCategory()
		{
			var faqCategory = CreateFaqCategory();
			faqCategory.Name = "updatedTest";
			_faqCategoryController.Update(faqCategory);

			Assert.AreEqual(_faqCategoryService.GetById(faqCategory.Id).Name, faqCategory.Name);
		}
		[Test]
		public void Delete_PasValidId_DeleteFaqCategory()
		{
			var faqCategory = CreateFaqCategory();
			var result = _faqCategoryController.Delete(faqCategory.Id);

			Assert.IsFalse(_faqCategoryService.Exists(i => i.Id == faqCategory.Id));
		}
		#region HelperMethods
		private FaqCategoryDTO CreateFaqCategory()
		{
			var faqCategory = new FaqCategoryDTO()
			{
				Name = "no name",
				IsActive = true
			};
			_faqCategoryService.Add(faqCategory);
			return faqCategory;
		}
		#endregion
	}
}
