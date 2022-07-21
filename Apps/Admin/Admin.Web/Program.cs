using Admin.Infrastructure.Ioc;
using FluentValidation.AspNetCore;
using FormHelper;
using System.Reflection;
public class Program
{
	public static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews()
					.AddFluentValidation(cfg =>
					{
						cfg.DisableDataAnnotationsValidation = true;
						cfg.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
					})
					.AddFormHelper();

		builder.Services.RegisterInfrastructure();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.UseFormHelper();

		app.MapControllerRoute(
			 name: "default",
			 pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}
