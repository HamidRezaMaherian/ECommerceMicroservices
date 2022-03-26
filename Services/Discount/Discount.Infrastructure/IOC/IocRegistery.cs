using Discount.Application.Configurations;
using Discount.Application.Services;
using Discount.Application.UnitOfWork;
using Discount.Infrastructure.Persist;
using Discount.Infrastructure.Persist.Mappings;
using Discount.Infrastructure.Tools;
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
			services.RegisterMapper();
		}
		private static void RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<IPercentDiscountService, PercentDiscountService>();
			services.AddScoped<IPriceDiscountService, PriceDiscountService>();
			services.AddScoped<IDiscountBaseService, DiscountBaseService>();
		}
		private static void RegisterPersistant(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
		private static void RegisterMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(PersistMapperProfile), typeof(ServiceMapper));
			services.AddScoped<ICustomMapper, CustomMapper>();
		}
	}
}
