using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persist;
using System;

namespace Product.Infrastructure.Tests.Utils
{
	public static class MockActions
	{
		public static ApplicationDbContext MockDbContext(string getName)
		{
			var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			dbOptionsBuilder.UseInMemoryDatabase("TestDb");
			return new ApplicationDbContext(dbOptionsBuilder.Options);
		}
		public static IMapper MockMapper(Action<IMapperConfigurationExpression> mapperConfigs)
		{
			return new Mapper(new MapperConfiguration(mapperConfigs));
		}
	}
}
