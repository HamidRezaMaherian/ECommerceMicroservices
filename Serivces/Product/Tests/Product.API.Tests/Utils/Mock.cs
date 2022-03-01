using Moq;
using Product.Application.Repositories;
using Product.Application.Services;
using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Product.API.Tests.Utils
{
	public static class MockAction<T, Tdto> where T : EntityPrimaryBase<string>
	{
		public static IRepository<T> MockRepository(ICollection<T> list)
		{
			var store = new Mock<IRepository<T>>();
			store.Setup(i => i.Get()).Returns(list.AsQueryable());
			store.Setup(i => i.Add(It.IsAny<T>())).Callback<T>(c => list.Add(c));
			store.Setup(i => i.Update(It.IsAny<T>())).Callback<T>(c =>
			{
				var res = list.FirstOrDefault(i => i.Id.Equals(c.Id));
				res = c;
			});
			store.Setup(i => i.Delete(It.IsAny<object>())).Callback<object>(c =>
			{
				var res = list.FirstOrDefault(i => i.Id.Equals(c));
				list.Remove(res);
			});
			store.Setup(i => i.Get(It.IsAny<object>())).Returns<object>(o => list.FirstOrDefault(i => i.Id.Equals(o)));
			return store.Object;
		}

		public static Mock<TService> MockServie<TService>(ICollection<T> list) where TService : class, IBaseService<T, Tdto>
		{
			var store = new Mock<TService>();
			var mapper = UtilsExtension.CreateMapper<T, Tdto>();
			var repository = MockRepository(list);

			store.Setup(i => i.GetAll()).Returns(list);

			store.Setup(i => i.GetAll(It.IsAny<Expression<Func<T, bool>>>()))
				.Returns<Expression<Func<T, bool>>>(p => list.Where(p.Compile()));

			store.Setup(i => i.Add(It.IsAny<Tdto>()))
				.Callback<Tdto>(c => list.Add(mapper.Map<T>(c)));

			store.Setup(i => i.GetById(It.IsAny<object>()))
				.Returns<Tdto>(o => list.FirstOrDefault(i => i.Id == o.ToString()));

			store.Setup(i => i.Update(It.IsAny<Tdto>()))
				.Callback<Tdto>(c => repository.Update(mapper.Map<T>(c)));

			store.Setup(i => i.Delete(It.IsAny<object>()))
				.Callback<object>(c => repository.Delete(c));

			return store;
		}

		public static TService MockServie<TService>(ICollection<T> list, Action<Mock<TService>> setups) where TService : class, IBaseService<T, Tdto>
		{
			var store=MockServie<TService>(list);
			setups?.Invoke(store);
			return store.Object;
		}
	}
}
