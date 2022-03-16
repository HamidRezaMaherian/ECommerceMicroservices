using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persist;
using System;

namespace Product.API.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(string dbName)
		{
			var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			dbOptionsBuilder.UseInMemoryDatabase(dbName);
			return new ApplicationDbContext(dbOptionsBuilder.Options);
		}
		public static IMapper MockMapper(Action<IMapperConfigurationExpression> mapperConfigs)
		{
			return new Mapper(new MapperConfiguration(mapperConfigs));
		}
	}
}
