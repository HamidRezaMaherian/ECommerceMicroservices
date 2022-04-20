using Consul;

public static class Extensions
{
	public static IServiceCollection ConfigureConsul(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IConsulClient, ConsulClient>();

		using var services = serviceCollection.BuildServiceProvider();
		var consulClient = services.GetService<IConsulClient>();
		var configuration = services.GetService<IConfiguration>();

		foreach (var item in Statics.Discoveries)
		{
			consulClient?.Agent.ServiceDeregister(item.ID).Wait();
			consulClient?.Agent.ServiceRegister(item).Wait();
		};
		return serviceCollection;
	}
}