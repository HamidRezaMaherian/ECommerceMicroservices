using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persist;
using Services.Shared.Contracts;
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
	}
}
