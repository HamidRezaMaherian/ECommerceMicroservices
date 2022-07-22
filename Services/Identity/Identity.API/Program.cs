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

		builder.Services.AddHealthChecks();
		builder.Services.AddIdentityServer(options =>
		{
			options.Events.RaiseErrorEvents = true;
			options.Events.RaiseInformationEvents = true;
			options.Events.RaiseFailureEvents = true;
			options.Events.RaiseSuccessEvents = true;
			options.EmitStaticAudienceClaim = true;
		})
		.AddAspNetIdentity<IdentityUser>()
		.AddInMemoryIdentityResources(Config.IdentityResources)
		.AddDeveloperSigningCredential()
		.AddInMemoryClients(Config.Clients);
		//.AddProfileService<IdentityProfile>();
		RegisterInstances(builder);
		var app = builder.Build();
		app.UseHttpLogging();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/home/error");
		}
		app.UseStaticFiles();

		builder.Services.AddServiceDiscovery("identity");

		app.UseIdentityServer();
		app.UseAuthorization();

		app.MapHealthChecks("/health");
		app.MapRazorPages();

		app.Run();
	}
	private static void MigrateDatabase(IServiceCollection services)
	{
		ApplicationDbContext db;
		using var serviceProvider = services.BuildServiceProvider();
		db = serviceProvider.GetService<ApplicationDbContext>();
		if (db?.Database.GetPendingMigrations().Any() ?? false)
		{
			db.Database.Migrate();
		}
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
					Name = "identity",
					Address = url.Host,
					Port = url.Port
				}).Result;
			}
		}
	}

}
