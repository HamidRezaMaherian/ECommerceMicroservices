using Consul;

public static class Extensions
{
	public static IServiceCollection ConfigureConsul(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IConsulClient, ConsulClient>();

		using var services = serviceCollection.BuildServiceProvider();
		var consulClient = services.GetService<IConsulClient>();
		var configuration = services.GetService<IConfiguration>();

		return serviceCollection;
	}
}