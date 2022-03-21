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
	public class StockControllerTests
	{
		private IStockService _stockService;
		private StockController _stockController;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var _stocks = new List<Stock>();
			_stockService = MockAction<Stock, StockDTO>
				.MockServie<IStockService>(_stocks).Object;
			_stockController = new StockController(_stockService);
		}
		[Test]
		public void GetAllForProduct_ReturnAllStocksForProduct()
		{
			var productId = Guid.NewGuid().ToString();
			var stock = new StockDTO()
			{
				ProductId = productId,
				Count = 23,
				IsActive = true,
				StoreId = Guid.NewGuid().ToString()
			};
			_stockService.Add(stock);

			var res = _stockController.GetAllForProduct(productId);

			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _stockService.GetAll(i => i.ProductId == productId).Select(i => i.Id));
		}
		[Test]
		public void GetAllForStore_ReturnAllStocksForStore()
		{
			var storeId = Guid.NewGuid().ToString();
			var stock = new StockDTO()
			{
				StoreId = storeId,
				Count = 23,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString()
			};
			_stockService.Add(stock);

			var res = _stockController.GetAllForStore(storeId);

			CollectionAssert.AreEquivalent(res.Value?.Select(i => i.Id), _stockService.GetAll(i => i.StoreId == storeId).Select(i => i.Id));
		}
		[Test]
		public void Create_AddStock()
		{
			var storeId = Guid.NewGuid().ToString();
			var stock = new StockDTO()
			{
				StoreId = storeId,
				Count = 23,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString()
			};
			var res = _stockController.Create(stock);

			Assert.IsNotNull(_stockService.GetById(stock.Id));
			Assert.AreEqual(res.GetType(), typeof(OkResult));
		}
		[Test]
		public void Update_UpdateStock()
		{
			var stockDTO = new StockDTO()
			{
				StoreId = Guid.NewGuid().ToString(),
				Count = 23,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString()
			};
			_stockService.Add(stockDTO);
			stockDTO.Count += 1;
			_stockController.Update(stockDTO);

			var stock = _stockService.GetById(stockDTO.Id);
			Assert.IsNotNull(stock);
			Assert.AreEqual(stock.Count, stockDTO.Count);
		}
		[Test]
		public void Delete_DeleteStock()
		{
			var stockDTO = new StockDTO()
			{
				StoreId = Guid.NewGuid().ToString(),
				Count = 23,
				IsActive = true,
				ProductId = Guid.NewGuid().ToString()
			};
			_stockService.Add(stockDTO);
			var res = _stockController.Delete(stockDTO.Id);

			Assert.IsFalse(_stockService.Exists(i => i.Id == stockDTO.Id));
		}
	}
}
