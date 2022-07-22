using Admin.Application.Services.UI;
using Admin.Application.UnitOfWork.UI;
using Admin.Infrastructure.APIUtils;
using Admin.Infrastructure.Services.UI;
using Admin.Infrastructure.UnitOfWork;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Infrastructure.Ioc
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection serviceCollection)
		{
			using var serviceProvider = serviceCollection.BuildServiceProvider();
			var configuration = serviceProvider.GetService<IConfiguration>();
			serviceCollection.AddServiceDiscovery("adminapp");
			serviceCollection.AddHttpClient(nameof(GatewayHttpClient), (_, opt) =>
			 {
				 opt.BaseAddress = new Uri("http://apigateway");
			 })
			 .AddClientServiceDiscovery();

			serviceCollection.AddScoped<HttpClientHelper<GatewayHttpClient>>();
			serviceCollection.AddScoped<ISliderService, SliderService>();
			serviceCollection.AddScoped<IUIUnitOfWork>(builder =>
			{
				return new UIUnitOfWork(serviceCollection.BuildServiceProvider());
			});
		}
	}
}
