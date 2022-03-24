using Inventory.API.Configurations.Validations;
using Inventory.API.Controllers;
using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Services.Shared.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using static Services.Shared.Tests.TestUtilsExtension;

namespace Inventory.API.Tests.Unit
{
	[TestFixture]
	public class StoreControllerTests
	{
		private IStoreService _storeService;
		private StoreController _storeController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _stores = new List<Store>();
			_storeService = MockAction<Store, StoreDTO>
				.MockServie<IStoreService>(_stores).Object;
			_storeController = new StoreController(_storeService);
		}
		[Test]
		public void GetAll_ReturnAll()
		{
			var store = new StoreDTO()
			{
				Name = "test",
				ShortDesc = "no descs",
				Description = "no desc",
				IsActive = true,
			};
			_storeService.Add(store);

			var res = _storeController.GetAll();

			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _storeService.GetAll().Select(i => i.Id));
		}

		[Test]
		public void Create_AddStock()
		{
			var store = new StoreDTO()
			{
				Name="test",
				ShortDesc="no descs",
				Description="no desc",
				IsActive = true,
			};
			var res = _storeController.Create(store);

			Assert.IsNotNull(_storeService.GetById(store.Id));
			Assert.AreEqual(res.GetType(), typeof(OkResult));
		}
		[Test]
		public void Update_UpdateStock()
		{
			var storeDTO = new StoreDTO()
			{
				Name = "test",
				ShortDesc = "no descs",
				Description = "no desc",
				IsActive = true,
			};
			_storeService.Add(storeDTO);
			storeDTO.Name = "updated test";
			var res = _storeController.Update(storeDTO);

			var store = _storeService.GetById(storeDTO.Id);
			Assert.IsNotNull(store);
			Assert.AreEqual(store.Name, storeDTO.Name);
		}
		[Test]
		public void Delete_DeleteStock()
		{
			var storeDTO = new StoreDTO()
			{
				Name = "test",
				ShortDesc = "no descs",
				Description = "no desc",
				IsActive = true,
			};
			_storeService.Add(storeDTO);
			var res = _storeController.Delete(storeDTO.Id);

			Assert.IsFalse(_storeService.Exists(i => i.Id == storeDTO.Id));
		}
	}
}
