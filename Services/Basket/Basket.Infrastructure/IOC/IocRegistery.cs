using Basket.Application.Repositories;
using Basket.Application.Services;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services)
		{
			services.RegisterPersistant();
			services.RegisterMapper();
		}
		private static void RegisterPersistant(this IServiceCollection services)
		{
			services.AddScoped<IShopCartRepo, ShopCartRepo>();
		}
		private static void RegisterMapper(this IServiceCollection services)
		{
			services.AddScoped<ICustomMapper, CustomMapper>();
		}
	}
}
