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
		builder.Services.AddServiceDiscoveryRegistration();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.RegisterInfrastructure(builder.Configuration);

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
