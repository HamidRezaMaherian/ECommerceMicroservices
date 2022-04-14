using NUnit.Framework;
using Order.Application.DTOs;
using Order.Application.Exceptions;
using Order.Application.Services;
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
		private IOrderService _orderService;
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
		#region ItemMethods
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
		[Test]
		public void DeleteItem_PassValidId_DeleteEntity()
		{

		}
		[Test]
		public void DeleteItem_PassInvalidId_DeleteEntity()
		{

		}
		#endregion

		#region DeliveryMethods
		[Test]
		public void UpsertDelivery_PassValidNotExistObject_InsertDelivery()
		{
			var order = CreateOrderObject();
			var deliveryDTO = new DeliveryDTO()
			{
				DeliverDateTime = DateTime.Now,
				Address = "no address",
				DeliverPrice = 209348,
				FirstName = "no name",
				LastName = "no name",
				EmailAddress = "test@test",
				State = "tehran"
			};
			_orderService.UpsertDelivery(order.Id, deliveryDTO);
			Assert.IsTrue(_unitOfWork.OrderRepo.DeliveryExists(order.Id));
		}
		[Test]
		public void UpsertDelivery_PassInvalidNotExistObject_InsertDelivery()
		{
			var order = CreateOrderObject();
			var deliveryDTO = new DeliveryDTO();
			_orderService.UpsertDelivery(order.Id, deliveryDTO);
			Assert.IsFalse(_unitOfWork.OrderRepo.DeliveryExists(order.Id));
		}
		[Test]
		public void UpsertDelivery_PassValidExistObject_UpdateDelivery()
		{
			var order = CreateOrderObject();
			var delivery = new Delivery()
			{
				DeliverDateTime = DateTime.Now,
				Address = "no address",
				DeliverPrice = 209348,
				FirstName = "no name",
				LastName = "no name",
				EmailAddress = "test@test",
				State = "tehran"
			};
			_unitOfWork.OrderRepo.AddDelivery(order.Id, delivery);
			_orderService.UpsertDelivery(order.Id, _mapper.Map<DeliveryDTO>(delivery));
			Assert.IsTrue(_unitOfWork.OrderRepo.DeliveryExists(order.Id));
		}
		[Test]
		public void UpsertDelivery_PassInvalidExistObject_UpdateDelivery()
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

		#endregion

		#region PaymentMethods
		[Test]
		public void UpsertPayment_PassValidNotExistObject_InsertPayment()
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
		public void UpsertPayment_PassInvalidNotExistObject_InsertPayment()
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
		public void UpsertPayment_PassValidExistObject_UpdatePayment()
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
		public void UpsertPayment_PassInvalidExistObject_UpdatePayment()
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
		#endregion

		#region HelperMethods

		private Domain.Entities.Order CreateOrderObject()
		{
			var order = new Domain.Entities.Order()
			{
				UserName = Guid.NewGuid().ToString(),
			};
			_unitOfWork.OrderRepo.Add(order);
			return order;
		}

		#endregion
	}
}
