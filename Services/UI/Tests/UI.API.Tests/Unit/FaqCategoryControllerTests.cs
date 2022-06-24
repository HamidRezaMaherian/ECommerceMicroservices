using NUnit.Framework;
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
	public class FaqCategoryControllerTests
	{
		private IFaqCategoryService _faqCategoryService;
		private ICustomMapper _customMapper;
		private FaqCategoryController _faqCategoryController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _faqCategorys = new List<FaqCategory>();
			_customMapper = CreateMapper(new TestMapperProfile());
			_faqCategoryService = MockAction<FaqCategory, FaqCategoryDTO>
				.MockServie<IFaqCategoryService>(_faqCategorys).Object;
			_faqCategoryController = new FaqCategoryController(_faqCategoryService);
		}
		[Test]
		public void GetAll_ReturnAllUIs()
		{
			CreateFaqCategory();
			var res = _faqCategoryController.GetAll();
			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _faqCategoryService.GetAll().Select(i => i.Id));
		}
		[Test]
		public void Create_PasValidEntity_AddFaqCategory()
		{
			var faqCategory = new CreateFaqCategoryDTO()
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
			var faqCategory = _customMapper.Map<UpdateFaqCategoryDTO>(CreateFaqCategory());
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
		private FaqCategory CreateFaqCategory()
		{
			var faqCategory = new CreateFaqCategoryDTO()
			{
				Name = "no name",
				IsActive = true
			};
			_faqCategoryService.Add(faqCategory);
			return _faqCategoryService.GetById(faqCategory.Id);
		}
		#endregion
	}
}
