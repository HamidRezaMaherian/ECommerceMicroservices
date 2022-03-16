﻿using AutoMapper;
using Inventory.Application.Repositories;
using Inventory.Application.UnitOfWork;
using Inventory.Infrastructure.Repositories;
using Services.Shared.Contracts;

namespace Inventory.Infrastructure.Persist
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;

		public UnitOfWork(ApplicationDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}
		#region Product
		private IStockRepo _stockRepo;
		public IStockRepo StockRepo
		{
			get
			{
				_stockRepo ??= new StockRepo(_db, _mapper);
				return _stockRepo;
			}
		}
		private IStoreRepo _storeRepo;
		public IStoreRepo StoreRepo
		{
			get
			{
				_storeRepo ??= new StoreRepo(_db, _mapper);
				return _storeRepo;
			}
		}
		#endregion

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

		#endregion
	}
}