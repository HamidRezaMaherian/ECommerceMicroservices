using Inventory.Application.UnitOfWork;
using Inventory.Infrastructure.Persist;
using Inventory.Infrastructure.Persist.Mappings;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
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
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
