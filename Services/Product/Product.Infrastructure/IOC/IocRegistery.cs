using Microsoft.Extensions.DependencyInjection;
using Product.Application.Repositories;
using Product.Application.Services;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Repositories;

namespace Product.Infrastructure.IOC
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
