using Order.Application.Repositories;
using Order.Application.Tools;
using Order.Application.UnitOfWork;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure.Persist
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		private readonly ICustomMapper _mapper;

		private IOrderRepo _orderRepo;
		public IOrderRepo OrderRepo
		{
			get
			{
				_orderRepo ??= new OrderRepo(_db, _mapper);
				return _orderRepo;
			}
		}
		private IOrderItemRepo _orderItemRepo;
		public IOrderItemRepo OrderItemRepo
		{
			get
			{
				_orderItemRepo ??= new OrderItemRepo(_db, _mapper);
				return _orderItemRepo;
			}
		}
		private IDeliveryRepo _deliveryRepo;
		public IDeliveryRepo DeliveryRepo
		{
			get
			{
				_deliveryRepo ??= new DeliveryRepo(_db, _mapper);
				return _deliveryRepo;
			}
		}
		private IPaymentRepo _paymentRepo;
		public IPaymentRepo PaymentRepo
		{
			get
			{
				_paymentRepo ??= new PaymentRepo(_db, _mapper);
				return _paymentRepo;
			}
		}

		public UnitOfWork(ApplicationDbContext db, ICustomMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}
		#region Methods
		public IRepository<T> GetRepo<T>()
			where T : class
		{
			var props = GetType().GetProperties();
			var res = props.FirstOrDefault(i => i.PropertyType.GetInterfaces().Contains(typeof(IRepository<T>)))?.GetValue(this, null);
			return res as IRepository<T>;
		}
		public void Dispose()
		{
			_db.Dispose();
		}

		public async ValueTask DisposeAsync()
		{
			await _db.DisposeAsync();
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		#endregion
	}
}
