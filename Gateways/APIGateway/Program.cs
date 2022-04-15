using Ocelot.DependencyInjection;
using Ocelot.Middleware;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Configuration.AddJsonFile($"ocelt.json");
		builder.Services.AddOcelot();
		var app = builder.Build();
		app.UseOcelot().Wait();
		app.Run();
	}
}
