using FileActor.AspNetCore;
using FluentValidation.AspNetCore;
using Product.Infrastructure.IOC;
using System.Reflection;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllers()
			.AddFluentValidation(cfg =>
			{
				cfg.DisableDataAnnotationsValidation = true;
				cfg.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
			});
		builder.Services.AddFileActor()
			.AddInMemoryContainer()
			.AddAttributeConfiguration()
			.AddLocalActor("lc", builder.Environment.WebRootPath);

		builder.Services.AddHealthChecks();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.RegisterInfrastructure(builder.Configuration);

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
		app.MapHealthChecks("/health");
		app.MapControllers();
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
					Name = "product",
					Address = url.Host,
					Port = url.Port
				}).Result;
			}
		}
	}

}
