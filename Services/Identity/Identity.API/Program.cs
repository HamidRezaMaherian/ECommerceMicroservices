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
		builder.Services.AddRazorPages();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});
		MigrateDatabase(builder.Services);
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
		//.AddTestUsers(Config.TestUsers.ToList())
		.AddInMemoryIdentityResources(Config.IdentityResources)
		.AddDeveloperSigningCredential()
		.AddInMemoryApiScopes(Config.ApiScopes)
		.AddInMemoryClients(Config.Clients);

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
		app.UseIdentityServer();

		app.MapControllers();
		app.MapRazorPages();
		app.MapHealthChecks("/health");

		app.Run();
	}
	private static void MigrateDatabase(IServiceCollection services)
	{
		ApplicationDbContext db;
		using var serviceProvider = services.BuildServiceProvider();
		db = serviceProvider.GetService<ApplicationDbContext>();
		if (db.Database.GetPendingMigrations().Any())
		{
			db.Database.Migrate();
		}
	}
}
