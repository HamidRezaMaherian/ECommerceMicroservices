using FluentValidation.AspNetCore;
using System.Reflection;
using Discount.Infrastructure.IOC;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();
		// Add services to the container.

		builder.Services.AddControllers()
			.AddFluentValidation(cfg =>
			 {
				 cfg.DisableDataAnnotationsValidation = true;
				 cfg.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
			 });

		builder.Services.RegisterInfrastructure();
		builder.Services.AddHealthChecks();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddGrpc();

		RegisterInstances(builder);

		var app = builder.Build();
		app.UseHttpLogging();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthorization();

		app.MapControllers();
		app.MapHealthChecks("/health");
		app.Run();
	}
	private static void RegisterInstances(WebApplicationBuilder builder)
	{
		var httpClient = new HttpClient();
		httpClient.BaseAddress = new Uri(builder.Configuration["ServiceDiscoveryURL"].ToString());
		var urls = builder.Configuration["ASPNETCORE_URLS"];
		foreach (var item in urls.Split(';'))
		{
			var url = new Uri(item);
			_ = httpClient.PostAsJsonAsync("/service/register", new
			{
				Name = "discount",
				Address = url.Host,
				Port = url.Port
			}).Result;
		}
	}
}
