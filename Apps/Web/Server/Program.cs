using Consul;
using Microsoft.AspNetCore.Authentication;
using WebApp.Shared.Ioc;
public class Program
{
	public static async Task Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();
		builder.Services.AddBff();
		builder.Services.RegisterSharedServices();

		var identityAddress = await GetIdentityAddress(builder.Services);

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = "cookie";
			options.DefaultChallengeScheme = "oidc";
			options.DefaultSignOutScheme = "oidc";
		})
			.AddCookie("cookie", options =>
			 {
				 options.Cookie.Name = "__Host-blazor";
				 options.Cookie.SameSite = SameSiteMode.Strict;
			 })
			.AddOpenIdConnect("oidc", (options) =>
			 {

				 options.Authority = identityAddress ?? "https://localhost:5005";
				 options.ClientId = "webapp";
				 options.ClientSecret = "webapp-secret";
				 options.Scope.Clear();
				 options.Scope.Add("openid");
				 options.Scope.Add("profile");
				 options.Scope.Add("user-claims");
				 options.Scope.Add("offline_access");
				 options.ResponseType = "code";
				 options.MapInboundClaims = true;
				 options.GetClaimsFromUserInfoEndpoint = true;
				 options.SaveTokens = true;
				 options.ClaimActions.MapAll();
			 });
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseBlazorFrameworkFiles();
		app.UseStaticFiles();

		app.UseRouting();
		app.UseAuthentication();
		app.UseBff();
		app.UseAuthorization();


		app.MapBffManagementEndpoints();
		app.MapRazorPages();

		app.MapControllers()
			 .RequireAuthorization()
			 .AsBffApiEndpoint();

		app.MapGet("/auth", async (context) =>
		{
			var token = await context.GetTokenAsync("id_token");
		});
		app.MapFallbackToPage("/_Host");

		app.Run();
	}
	static async Task<string> GetIdentityAddress(IServiceCollection services)
	{
		string identityAddress = null;
		try
		{
			identityAddress = await services.BuildServiceProvider()
		.GetService<IConsulClient>()
		.GetRequestUriAsync("identity");
		}
		catch { }
		return identityAddress;
	}
}
