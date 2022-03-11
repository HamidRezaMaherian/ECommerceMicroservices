﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Services;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Services;

namespace Product.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddAutoMapper(typeof(PersistMapperProfile));
			services.RegisterServices();
			services.RegisterPersistant(configuration);

		}
		private static void RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductCategoryService, ProductCategoryService>();
		}
		private static void RegisterPersistant(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(cfg =>
			{
				cfg.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
