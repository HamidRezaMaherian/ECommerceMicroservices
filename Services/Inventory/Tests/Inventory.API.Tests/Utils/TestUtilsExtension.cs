using AutoMapper;
using Inventory.Application.Repositories;
using Inventory.Application.Services;
using Inventory.Application.Tools;
using Inventory.Infrastructure.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Services.Shared.AppUtils;
using Inventory.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Inventory.API.Tests.Utils
{
	public static class TestUtilsExtension
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
				store.Setup(i => i.Get(It.IsAny<object>())).Returns<object>(o => list.First(i => i.Id.Equals(o)));
				return store.Object;
			}

			public static Mock<TService> MockServie<TService>(ICollection<T> list) where TService : class, IBaseService<T, Tdto>
			{
				var store = new Mock<TService>();
				var mapper = CreateMapper<T, Tdto>();
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
					.Returns<object>(o => list.First(i => i.Id == o.ToString()));

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

		public static ICustomMapper CreateMapper<T1, T2>()
		{
			var automapperConfig = new MapperConfiguration(i => i.CreateMap<T1, T2>().ReverseMap());

			return new CustomMapper(new AutoMapper.Mapper(automapperConfig));
		}
		public static ICustomMapper CreateMapper(params Profile[] profiles)
		{
			var automapperConfig = new MapperConfiguration(i => i.AddProfiles(profiles));
			return new CustomMapper(new AutoMapper.Mapper(automapperConfig));
		}
	}
	public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
	{
		private readonly Action<IServiceCollection> _mockConfigureServices;

		public TestingWebAppFactory(Action<IServiceCollection> mockConfigureServices)
		{
			_mockConfigureServices = mockConfigureServices;
		}
		public TestingWebAppFactory()
		{

		}
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			if (_mockConfigureServices != null)
				builder.ConfigureServices(_mockConfigureServices);
			else
				base.ConfigureWebHost(builder);
		}
	}
}
