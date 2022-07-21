using FluentValidation.AspNetCore;
using System.Reflection;
using FileActor.AspNetCore;
using UI.Infrastructure.IOC;
public class Program
{
	public static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddHealthChecks();
		builder.Services.AddControllers()
			.AddFluentValidation(cfg =>
			{
				cfg.DisableDataAnnotationsValidation = true;
				cfg.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
			});
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.RegisterInfrastructure();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddFileActor()
			.AddInMemoryContainer()
			.AddObjectConfigurations(Assembly.GetAssembly(typeof(UI.Application.DTOs.SliderDTO)))
			.AddLocalActor("lc", builder.Environment.WebRootPath);

		RegisterInstances(builder);
		var app = builder.Build();
		app.UseHttpLogging();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseStaticFiles();

		app.UseAuthorization();

		app.MapControllers();
		app.MapHealthChecks("/health");
		app.Run();
	}
	private static void RegisterInstances(WebApplicationBuilder builder)
	{
		if (builder.Environment.IsDevelopment())
		{
			var httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri(builder.Configuration["ServiceDiscoveryURL"].ToString());
			var urls = builder.Configuration["ASPNETCORE_URLS"];
			foreach (var item in urls.Split(';'))
			{
				var url = new Uri(item);
				var result = httpClient.PostAsJsonAsync("/service/register", new
				{
					Name = "ui",
					Address = url.Host,
					Port = url.Port
				}).Result;
			}
		}
	}

}
