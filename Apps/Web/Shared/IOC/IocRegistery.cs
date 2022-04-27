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
			var configuration = serviceProvider.GetService<IConfiguration>();
			serviceCollection.AddHttpClient<GatewayHttpClient>(async opt =>
			{
				var serviceDiscoveryClient = serviceProvider.GetService<IConsulClient>();

				opt.BaseAddress = new Uri(await serviceDiscoveryClient.GetRequestUriAsync("gateway"));
			});
			serviceCollection.AddSingleton<HttpClientHelper<GatewayHttpClient>>();
			serviceCollection.AddSingleton<IConsulClient>((cfg) =>
			{
				return new ConsulClient(opt =>
				{
					opt.Address = new Uri(configuration?["ServiceDiscovery:Address"] ?? "http://localhost:8500");
				});
			});
		
			serviceCollection.AddScoped<IUIService, UIService>();
		}
		public static async Task<string> GetRequestUriAsync(this IConsulClient serviceDiscoveryClient, string serviceName)
		{
			//Get all services registered on Consul
			var allRegisteredServices = await serviceDiscoveryClient.Agent.Services();

			//Get all instance of the service went to send a request to
			var registeredServices = allRegisteredServices.Response?.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToList();

			//Get a random instance of the service
			var service = GetRandomInstance(registeredServices, serviceName);

			var uriBuilder = new UriBuilder()
			{
				Host = service.Address,
				Port = service.Port
			};

			return uriBuilder.Uri.AbsoluteUri;
		}
		private static AgentService GetRandomInstance(IList<AgentService> services, string serviceName)
		{
			Random _random = new();
			AgentService servToUse = services[_random.Next(0, services.Count)];
			return servToUse;
		}
	}
}
