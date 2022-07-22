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

		builder.Services.AddServiceDiscovery("basket");

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
}