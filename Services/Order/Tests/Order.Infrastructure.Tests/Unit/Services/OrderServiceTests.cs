using NUnit.Framework;
using Order.Application.Exceptions;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;
using Order.Infrastructure.Services;
using Order.Infrastructure.Tests.Utils;
using System;

namespace Order.Infrastructure.Tests.Unit.Repositories
{
	[TestFixture]
	public class OrderServiceTests
	{
		private OrderService _orderService;
		private ICustomMapper _mapper;
		private UnitOfWork _unitOfWork;
		[SetUp]
		public void Setup()
		{
			_mapper = TestUtilsExtension.CreateMapper<Domain.Entities.Order, OrderDAO>();
			var unitOfWork = new UnitOfWork(MockActions.MockDbContext("TestDb"), _mapper);
			_orderService = new OrderService(unitOfWork, _mapper);
		}
		[TearDown]
		public void TearDown()
		{
			_unitOfWork?.Dispose();
		}
		[Test]
		public void AddItem_PassValidItem_InsertItem()
		{
			var order = CreateOrderObject();
			var orderItem = new OrderItemDTO()
			{
				ProductId = Guid.NewGuid().ToString(),
				PropertyId = Guid.NewGuid().ToString(),
				Count = 1,
				Price = 4500
			};
			_orderService.AddItem(order.Id, orderItem);
			Assert.IsTrue(_unitOfWork.OrderRepo.ItemExists(order.Id));
		}
		[Test]
		public void AddItem_PassInvalidItem_ThrowException()
		{
			var order = CreateOrderObject();

			Assert.Throws<InsertOperationException>(() =>
			{
				_orderService.AddItem(order.Id, null);
			});
			Assert.IsFalse(_unitOfWork.OrderRepo.ItemExists(order.Id));
		}
		[Test]
		public void UpdateItem_PassValidItem_InsertItem()
		{
			var order = CreateOrderObject();
			var orderItem = new OrderItem()
			{
				ProductId = Guid.NewGuid().ToString(),
				PropertyId = Guid.NewGuid().ToString(),
				Count = 1,
				Price = 4500
			};
			_unitOfWork.OrderRepo.AddItem(order.Id, orderItem);
			var orderItemDTO = _mapper.Map<OrderItemDTO>(orderItem);
			orderItemDTO.Count++;

			_orderService.UpdateItem(orderItemDTO);
			Assert.AreEqual(orderItemDTO.Count, _unitOfWork.OrderItemRepo.Get(orderItem.Id).Count);
		}
		[Test]
		public void UpdateItem_PassInvalidItem_ThrowException()
		{
			var order = CreateOrderObject();
			var orderItem = new OrderItem()
			{
				ProductId = Guid.NewGuid().ToString(),
				PropertyId = Guid.NewGuid().ToString(),
				Count = 1,
				Price = 4500
			};
			_unitOfWork.OrderRepo.AddItem(order.Id, orderItem);
			var orderItemDTO = new OrderItemDTO()
			{
				Id = orderItem.Id,
				Count = ++orderItem.Count
			};

			_orderService.UpdateItem(orderItemDTO);
			Assert.AreNotEqual(orderItemDTO.Count, _unitOfWork.OrderItemRepo.Get(orderItem.Id).Count);
		}
		private Domain.Entities.Order CreateOrderObject()
		{
			var order = new Domain.Entities.Order()
			{
				UserName = Guid.NewGuid().ToString(),
			};
			_unitOfWork.OrderRepo.Add(order);
				return order;
		}
	}
}
