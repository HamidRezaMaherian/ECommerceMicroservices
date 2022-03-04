using Discount.Application.UnitOfWork;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure.IOC
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
