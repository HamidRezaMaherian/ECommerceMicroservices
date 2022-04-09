using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Order.Application.Exceptions;
using Order.Application.Tools;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;
using Order.Infrastructure.Persist.Mappings;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Tests.Utils;
using Services.Shared.AppUtils;
using System;
using System.Linq;

namespace Order.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class OrderRepoTests
	{
		private OrderRepo _orderRepo;
		private ICustomMapper _mapper;
		private ApplicationDbContext _db;
		[SetUp]
		public void Setup()
		{
			_db?.Dispose();
			_db = MockActions.MockDbContext("TestDb");
			_mapper = TestUtilsExtension.CreateMapper(new PersistMapperProfile());
			_orderRepo = new OrderRepo(_db, _mapper);
		}
		[Test]
		public void Add_PasValidEntity_CreateEntity()
		{
			var order = new Domain.Entities.Order()
			{
				UserName="testUserName",
				IsActive = true,
			};
			_orderRepo.Add(order);
			_db.SaveChanges();
			Assert.AreEqual(_db.Orders.Count(i => i.Id == order.Id), 1);
		}
		[Test]
		public void Add_PasInvalidEntity_ThrowException()
		{
			var order = new Domain.Entities.Order()
			{
			};
			Assert.Throws<InsertOperationException>(() =>
			{
				_orderRepo.Add(order);
			});
			Assert.AreEqual(_db.Orders.Count(), 0);
		}
		[Test]
		public void Update_PasValidEntity_UpdateEntity()
		{
			var order = CreateMockObj();
			_db.Entry(order).State = EntityState.Detached;
			order.Name = "updatedTest";
			_orderRepo.Update(_mapper.Map<Domain.Entities.Order>(order));

			var updatedOrder = _db.Orders.Find(order.Id);
			Assert.AreEqual(updatedOrder?.Name, order.Name);
		}
		[Test]
		public void Update_PasInvalidEntity_ThrowException()
		{
			var order = CreateMockObj();
			var invalidOrder = new Domain.Entities.Order()
			{
				Id = order.Id,
				UserName = "updatedTest"
			};
			Assert.Throws<UpdateOperationException>(() =>
				{
					_orderRepo.Update(invalidOrder);
				});
			Assert.AreNotEqual(invalidOrder.UserName, order.UserName);
		}

		[Test]
		public void Delete_PasValidId_DeleteEntity()
		{
			var slider = CreateMockObj();

			_orderRepo.Delete(slider.Id);
			_db.SaveChanges();
			Assert.IsFalse(_db.Orders.AsQueryable().Any(i => i.Id == slider.Id));
		}
		[Test]
		public void Delete_PasInvalidId_ThrowException()
		{
			Assert.Throws<DeleteOperationException>(() =>
			{
				_orderRepo.Delete(Guid.NewGuid().ToString());
			});
		}
		[Test]
		public void Get_ReturnAllEntities()
		{
			foreach (var item in Enumerable.Range(1, 3))
			{
				CreateMockObj($"title{item}");
			}

			CollectionAssert.AreEquivalent(_db.Orders.AsQueryable().Select(i => i.Id).ToList(),
				_orderRepo.Get().Select(i => i.Id));
		}
		[Test]
		public void Get_PassValidQueryParam_ReturnFilteredEntities()
		{
			foreach (var item in Enumerable.Range(1, 10))
			{
				CreateMockObj($"title{item}");
			}
			QueryParams<Domain.Entities.Order> queryParams = new QueryParams<Domain.Entities.Order>
			{
				Expression = i => true,
				Skip = 1,
				Take = 5,
			};
			CollectionAssert.AreEquivalent(
				_db.Orders.AsQueryable().Skip(1).Take(5).Select(i => i.Id).ToList(),
				_orderRepo.Get(queryParams).Select(i => i.Id));
		}
		[Test]
		public void Get_PassInvalidQueryParam_ThrowException()
		{
			Assert.Throws<ReadOperationException>(() =>
			{
				_orderRepo.Get(null);
			});
		}

		#region HelperMethods
		private OrderDAO CreateMockObj()
		{
			var order = MockObj("test");
			var result = _db.Orders.Add(order);
			order.Id = result.Entity.Id;
			_db.SaveChanges();
			return order;
		}
		private OrderDAO CreateMockObj(string name)
		{
			var order = MockObj(name);
			var result = _db.Orders.Add(order);
			order.Id = result.Entity.Id;
			_db.SaveChanges();
			return order;
		}
		private OrderDAO MockObj(string name)
		{
			var order = new OrderDAO()
			{
				UserName = name,
				IsActive = true
			};
			return order;
		}
		#endregion

	}
}
