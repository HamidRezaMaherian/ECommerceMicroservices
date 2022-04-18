using Identity.API;
using Identity.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
	public static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});
		builder.Services.AddIdentity<IdentityUser, IdentityRole>()
					.AddDefaultTokenProviders()
					.AddEntityFrameworkStores<ApplicationDbContext>();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddHealthChecks();
		builder.Services.AddIdentityServer(options =>
		{
			options.Events.RaiseErrorEvents = true;
			options.Events.RaiseInformationEvents = true;
			options.Events.RaiseFailureEvents = true;
			options.Events.RaiseSuccessEvents = true;
			options.EmitStaticAudienceClaim = true;
		})
		.AddApiAuthorization<IdentityUser, ApplicationDbContext>(opt =>
		opt.SigningCredential = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.ASCII.GetBytes("KLJIWSFJLSJOGUWIOJKUWIOEL")), SecurityAlgorithms.HmacSha256)
		)
		.AddInMemoryIdentityResources(Config.IdentityResources)
		.AddDeveloperSigningCredential()
		.AddInMemoryApiScopes(Config.ApiScopes)
		.AddInMemoryClients(Config.Clients);

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();
		app.UseIdentityServer();

		app.MapControllers();
		app.MapHealthChecks("/health");

		app.Run();
	}
}
