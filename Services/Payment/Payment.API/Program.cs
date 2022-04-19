using Microsoft.EntityFrameworkCore;
using Parbad.Builder;
using Parbad.Gateway.ParbadVirtual;
using Parbad.Storage.EntityFrameworkCore.Builder;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddHealthChecks();
		builder.Services.AddParbad()
			.ConfigureGateways(gtw =>
			{
				gtw.AddParbadVirtual()
				.WithOptions(opt => opt.GatewayPath = "/paymentgtw");
			})
			.ConfigureStorage(stg =>
			{
				stg.UseEfCore(opt =>
				{
					var assemblyName = typeof(Program).Assembly.GetName().Name;
					opt.ConfigureDbContext = db => db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(assemblyName));
				});
			})
			.ConfigureHttpContext(opt => opt.UseDefaultAspNetCore());

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
		app.UseParbadVirtualGatewayWhenDeveloping();
		app.Run();
	}
}
