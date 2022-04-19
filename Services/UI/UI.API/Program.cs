using FluentValidation.AspNetCore;
using System.Reflection;
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
}
