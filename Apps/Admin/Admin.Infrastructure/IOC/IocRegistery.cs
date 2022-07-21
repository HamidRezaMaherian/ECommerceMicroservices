using Admin.Application.Services.UI;
using Admin.Application.UnitOfWork.UI;
using Admin.Infrastructure.APIUtils;
using Admin.Infrastructure.Services.UI;
using Admin.Infrastructure.UnitOfWork;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Admin.Infrastructure.Ioc
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection serviceCollection)
		{
			using var serviceProvider = serviceCollection.BuildServiceProvider();
			var configuration = serviceProvider.GetService<IConfiguration>();
			var serviceDiscoveryClient = new ConsulClient(opt =>
			{
				opt.Address = new Uri(configuration?["ServiceDiscovery:Address"] ?? "http://localhost:8500");
			});

			var gateWayUri = new Uri(serviceDiscoveryClient.GetRequestUriAsync("apigateway").Result);
			serviceCollection.AddHttpClient(nameof(GatewayHttpClient), (service, opt) =>
			 {
				 opt.BaseAddress = gateWayUri;
			 }).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, (_) => TimeSpan.FromSeconds(10)));

			serviceCollection.AddScoped<HttpClientHelper<GatewayHttpClient>>();
			serviceCollection.AddScoped<ISliderService, SliderService>();
			serviceCollection.AddScoped<IUIUnitOfWork>(builder =>
			{
				return new UIUnitOfWork(serviceCollection.BuildServiceProvider());
			});
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
				Scheme = "http",
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
