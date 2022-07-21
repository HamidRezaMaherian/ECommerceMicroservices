using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Configuration.AddJsonFile($"ocelot.json");
		builder.Services.AddOcelot().AddConsul().AddPolly();
		var app = builder.Build();
		app.UseOcelot().Wait();
		app.Run();
	}
}
