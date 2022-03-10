using Moq;
using Services.Shared.AppUtils;
using Services.Shared.Common;
using Services.Shared.Contracts;
using System.Linq.Expressions;

namespace Services.Shared.Tests
{
	public static class MockAction<T, Tdto> where T : EntityPrimaryBase<string>
	{
		public static IRepository<T> MockRepository(ICollection<T> list)
		{
			var store = new Mock<IRepository<T>>();
			store.Setup(i => i.Get()).Returns(list.AsQueryable());
			store.Setup(i => i.Add(It.IsAny<T>())).Callback((T c) =>
			{
				c.Id = Guid.NewGuid().ToString();
				list.Add(c);
			});
			store.Setup(i => i.Update(It.IsAny<T>())).Callback<T>(c =>
			{
				var res = list.FirstOrDefault(i => i.Id.Equals(c.Id));
				list.Remove(res);
				list.Add(c);
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

			store.Setup(i => i.GetAll()).Returns(repository.Get());

			store.Setup(i => i.GetAll(It.IsAny<Expression<Func<T, bool>>>()))
				.Returns<Expression<Func<T, bool>>>(p =>
				{
					return repository.Get(new QueryParams<T>()
					{
						Expression = p
					});
				});
			store.Setup(i => i.Add(It.IsAny<Tdto>()))
				.Callback<Tdto>((c) =>
				{
					var entity = mapper.Map<T>(c);
					repository.Add(entity);
					mapper.Map(entity, c);
				});

			store.Setup(i => i.GetById(It.IsAny<object>()))
				.Returns<object>(o => list.FirstOrDefault(i => i.Id == o.ToString()));

			store.Setup(i => i.Update(It.IsAny<Tdto>()))
				.Callback<Tdto>(c => repository.Update(mapper.Map<T>(c)));

			store.Setup(i => i.Delete(It.IsAny<object>()))
				.Callback<object>(c => repository.Delete(c));

			return store;
		}

		public static TService MockServie<TService>(ICollection<T> list, Action<Mock<TService>> setups) where TService : class, IBaseService<T, Tdto>
		{
			var store = MockServie<TService>(list);
			setups?.Invoke(store);
			return store.Object;
		}
	}
}
