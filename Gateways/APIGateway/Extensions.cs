using Consul;

public static class Extensions
{
	public static IServiceCollection ConfigureConsul(this IServiceCollection serviceCollection)
	{
		using var services = serviceCollection.BuildServiceProvider();
		var consulClient = services.GetService<IConsulClient>();
		var configuration = services.GetService<IConfiguration>();

		foreach (var item in Statics.Discoveries)
		{
			item.ID = $"{item.Name}:{Guid.NewGuid().ToString().Replace("-", "")[0]}";
			consulClient?.Agent.ServiceDeregister(item.ID).Wait();
			consulClient?.Agent.ServiceRegister(item).Wait();
		};
		return serviceCollection;
	}
}