using Inventory.Application.Configurations;
using Inventory.Application.Services;
using Inventory.Application.Tools;
using Inventory.Application.UnitOfWork;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.Mappings;
using Inventory.Infrastructure.Services;
using Inventory.Infrastructure.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Inventory.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services)
		{
			services.RegisterServices();
			services.RegisterPersistant();
			services.RegisterConfigurations();
		}
		private static void RegisterConfigurations(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(PersistMapperProfile), typeof(ServiceMapper));
			services.AddSingleton<ICustomMapper, CustomMapper>();
		}

		private static void RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<IStockService, StockService>();
			services.AddScoped<IStoreService, StoreService>();
		}
		private static void RegisterPersistant(this IServiceCollection services)
		{
			services.AddScoped<ApplicationDbContext>(provider =>
			{
				var mongoClient = provider.GetService<MongoClient>();
				return new ApplicationDbContext(mongoClient, "inventoryDb");
			});
			services.AddScoped(provider =>
			{
				var configuration = provider.GetService<IConfiguration>();
				return new MongoClient(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
