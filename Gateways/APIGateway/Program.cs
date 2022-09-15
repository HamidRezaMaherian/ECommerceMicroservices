using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Logging.RegisterLoggingProvider(builder.Configuration);
		builder.Services.AddHealthChecks();
		builder.Services.AddServiceDiscoveryRegistration();
		builder.Configuration.AddJsonFile($"ocelot.json");
		builder.Services.AddOcelot().AddConsul().AddPolly();
		builder.Services.AddCors();
		var app = builder.Build();
		app.UseHealthChecks("/health");
		app.UseCors();
		app.UseOcelot().Wait();
		app.Run();
	}
}
