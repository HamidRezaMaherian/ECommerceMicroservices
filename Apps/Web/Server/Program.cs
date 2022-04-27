using Consul;
using WebApp.Shared.APIUtils;
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
				 options.ClientId = "interactive.confidential";
				 options.ClientSecret = "secret";
				 options.ResponseType = "code";
				 options.ResponseMode = "query";

				 options.Scope.Clear();
				 options.Scope.Add("openid");
				 options.Scope.Add("profile");
				 options.Scope.Add("api");
				 options.Scope.Add("offline_access");

				 options.MapInboundClaims = false;
				 options.GetClaimsFromUserInfoEndpoint = true;
				 options.SaveTokens = true;
			 });
		builder.Services.RegisterSharedServices();
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


		app.MapBffManagementEndpoints();
		app.MapRazorPages();
		app.MapControllers()
			 .RequireAuthorization()
			 .AsBffApiEndpoint();
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
