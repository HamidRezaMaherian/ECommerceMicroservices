using Inventory.Application.UnitOfWork;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.Mappings;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(PersistMapperProfile));
			services.RegisterServices();
			services.RegisterPersistant();

		}
		private static void RegisterServices(this IServiceCollection services)
		{
			//services.AddScoped<IProductService,productservice>
		}
		private static void RegisterPersistant(this IServiceCollection services)
		{
			services.AddScoped<ApplicationDbContext>();
			services.AddScoped(provider =>
			{
				var configuration = provider.GetService<IConfiguration>();
				return new MongoClient(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
