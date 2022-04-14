using Order.Application.Exceptions;
using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Domain.Entities;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.DAOs;
using Services.Shared.AppUtils;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories
{
	public class OrderRepo : Repository<Domain.Entities.Order, OrderDAO>, IOrderRepo
	{
		public OrderRepo(ApplicationDbContext db, ICustomMapper mapper) : base(db, mapper)
		{
		}

		public void AddDelivery(string orderId, Delivery delivery)
		{
			if (!Exists(i => i.Id == orderId))
				throw new InvalidOperationException("Order id not exist");
			var entity = _mapper.Map<DeliveryDAO>(delivery);
			entity.OrderId = orderId;
			var result = _db.Deliveries.Add(entity);
			try
			{
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new InsertOperationException(e.Message, e.InnerException);
			}
			finally
			{
				DetachEntity(result.Entity);
			}
			_mapper.Map(result.Entity, entity);
		}

		public void AddItem(string orderId, OrderItem orderItem)
		{
			if (!Exists(i => i.Id == orderId))
				throw new InvalidOperationException("Order id not exist");
			var entity = _mapper.Map<OrderItemDAO>(orderItem);
			entity.OrderId = orderId;
			var result = _db.OrderItems.Add(entity);
			try
			{
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new InsertOperationException(e.Message, e.InnerException);
			}
			finally
			{
				DetachEntity(result.Entity);
			}
			_mapper.Map(result.Entity, entity);
		}

		public void AddPayment(string orderId, Payment payment)
		{
			if (!Exists(i => i.Id == orderId))
				throw new InvalidOperationException("Order Id not exist");
			var entity = _mapper.Map<PaymentDAO>(payment);
			entity.OrderId = orderId;
			var result = _db.Payments.Add(entity);
			try
			{
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new InsertOperationException(e.Message, e.InnerException);
			}
			finally
			{
				DetachEntity(result.Entity);
			}
			_mapper.Map(result.Entity, entity);
		}

		public void DeleteDelivery(string deliveryId)
		{
			try
			{
				var entity = _db.Deliveries.Find(deliveryId);
				_db.Deliveries.Remove(_mapper.Map<DeliveryDAO>(entity));
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}

		public void DeleteItem(string itemId)
		{
			try
			{
				var entity = _db.OrderItems.Find(itemId);
				_db.OrderItems.Remove(_mapper.Map<OrderItemDAO>(entity));
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}

		public void DeletePayment(string paymentId)
		{
			try
			{
				var entity = _db.Payments.Find(paymentId);
				_db.Payments.Remove(_mapper.Map<PaymentDAO>(entity));
			}
			catch (Exception e)
			{
				throw new DeleteOperationException(e.Message, e.InnerException);
			}
		}

		public bool DeliveryExists(string deliveryId, Expression<Func<Delivery, bool>> exp)
		{
			throw new NotImplementedException();
		}

		public bool DeliveryExists(string deliveryId)
		{
			throw new NotImplementedException();
		}

		public bool ItemExists(string orderId, Expression<Func<OrderItem, bool>> exp)
		{
			throw new NotImplementedException();
		}

		public bool ItemExists(string orderId)
		{
			throw new NotImplementedException();
		}

		public bool PaymentExists(string paymentId, Expression<Func<Payment, bool>> exp)
		{
			throw new NotImplementedException();
		}

		public bool PaymentExists(string paymentId)
		{
			throw new NotImplementedException();
		}

		public void UpdateDelivery(Delivery delivery)
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(OrderItem orderItem)
		{
			throw new NotImplementedException();
		}

		public void UpdatePayment(Payment payment)
		{
			throw new NotImplementedException();
		}
	}
}
