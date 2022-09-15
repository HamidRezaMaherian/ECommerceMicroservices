using FileActor.AspNetCore;
using FluentValidation.AspNetCore;
using Inventory.Infrastructure.IOC;
using System.Reflection;

public class Program
{
	public static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		builder.Logging.RegisterLoggingProvider(builder.Configuration);
		// Add services to the container.
		builder.Services.AddHealthChecks();
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

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.RegisterInfrastructure();

		builder.Services.AddServiceDiscoveryRegistration();
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
}
