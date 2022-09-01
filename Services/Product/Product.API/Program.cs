using FileActor.AspNetCore;
using FluentValidation.AspNetCore;
using Product.API.Configurations;
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
			.AddObjectConfigurations(Assembly.GetAssembly(typeof(Product.Application.DTOs.ProductDTO)))
			.AddLocalActor("lc", builder.Environment.WebRootPath);

		builder.Services.AddHealthChecks();
		builder.Services.AddServiceDiscoveryRegistration();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.RegisterInfrastructure(builder.Configuration)
			.AddCdnResolver<LocalCdnResolver>();

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
}
