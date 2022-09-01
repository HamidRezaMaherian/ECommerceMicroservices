using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Configurations;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Services;
using Product.Infrastructure.Tools;

namespace Product.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.RegisterServices();
			services.RegisterPersistant(configuration);
			services.RegisterConfigurations();
			return services;
		}
		public static void AddCdnResolver<T>(this IServiceCollection services) where T : class, ICdnResolver
		{
			services.AddSingleton<ICdnResolver, T>();
		}

		#region Private

		private static void RegisterConfigurations(this IServiceCollection services)
		{
			services.AddSingleton<ServiceMapper>();
			services.AddSingleton<PersistMapperProfile>();
			services.AddSingleton(provider =>
			{
				return new MapperConfiguration(cfg =>
				{
					cfg.AddProfile(provider.GetService<ServiceMapper>());
					cfg.AddProfile(provider.GetService<PersistMapperProfile>());
				}).CreateMapper();
			});
			services.AddSingleton<ICustomMapper, CustomMapper>();
		}
		private static void RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductCategoryService, ProductCategoryService>();
			services.AddScoped<IBrandService, BrandService>();
			services.AddScoped<IProductImageService, ProductImageService>();
		}
		private static void RegisterPersistant(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContextPool<ApplicationDbContext>(cfg =>
			{
				var connString = configuration.GetConnectionString("DefaultConnection");
				cfg.UseMySql(connString,ServerVersion.AutoDetect(connString));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.MigrateDatabase();
		}
		private static void MigrateDatabase(this IServiceCollection services)
		{
			ApplicationDbContext db;
			using var serviceProvider = services.BuildServiceProvider();
			db = serviceProvider.GetService<ApplicationDbContext>();
			if (db?.Database.GetPendingMigrations().Any()??false)
			{
				db.Database.Migrate();
			}
		}

		#endregion

	}
}
