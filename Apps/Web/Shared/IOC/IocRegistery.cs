using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Shared.APIUtils;
using WebApp.Shared.Services.Contracts;

namespace WebApp.Shared.Ioc
{
	public static class IocRegistery
	{
		public static void RegisterSharedServices(this IServiceCollection serviceCollection)
		{
			using var serviceProvider = serviceCollection.BuildServiceProvider();
			//serviceCollection.AddServiceDiscoveryRegistration();
			var configuration = serviceProvider.GetService<IConfiguration>();
			serviceCollection.AddHttpClient<GatewayHttpClient>((_, opt) =>
			{
				opt.BaseAddress = new Uri("http://apigateway");
			})
			.AddClientServiceDiscovery();

			serviceCollection.AddSingleton<HttpClientHelper<GatewayHttpClient>>();

			serviceCollection.AddScoped<IUIService, UIService>();
			serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
