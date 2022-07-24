using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

public static class Extensions
{
	public static IServiceCollection AddServiceDiscoveryRegistration(this IServiceCollection serviceCollection)
	{
		//var configBuilder = new ConfigurationBuilder();
		//configBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
		//{
		//	new KeyValuePair<string, string>("Consul:Discovery:HealthCheckPath",healthPath),
		//	new KeyValuePair<string, string>("Consul:Discovery:ServiceName",serviceName),
		//	new KeyValuePair<string, string>("Consul:Discovery:HealthCheckCriticalTimeout","2m"),
		//	new KeyValuePair<string, string>("Consul:Discovery:HealthCheckInterval","1m"),
		//});
		serviceCollection.AddDiscoveryClient();
		serviceCollection.AddServiceDiscovery(options => options.UseConsul());
		return serviceCollection;
	}
	public static IHttpClientBuilder AddClientServiceDiscovery(this IHttpClientBuilder httpClientBuilder)
	{
		httpClientBuilder.AddServiceDiscovery();
		return httpClientBuilder;
	}
}