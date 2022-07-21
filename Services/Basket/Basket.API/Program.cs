using Basket.Infrastructure.IOC;
using Microsoft.AspNetCore.Http.Features;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddHealthChecks();
		builder.Services.AddSwaggerGen();
		builder.Services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = builder.Configuration.GetValue<string>("ConnectionString");
		});
		builder.Services.RegisterInfrastructure();


		RegisterInstances(builder);

		var app = builder.Build();

		app.UseHttpLogging();

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
				Name = "basket",
				Address = url.Host,
				Port = url.Port
			}).Result;
		}
	}
}