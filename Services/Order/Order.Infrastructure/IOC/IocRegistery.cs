using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Configurations;
using Order.Application.Services;
using Order.Application.Tools;
using Order.Application.UnitOfWork;
using Order.Infrastructure.Persist;
using Order.Infrastructure.Persist.Mappings;
using Order.Infrastructure.Services;
using Order.Infrastructure.Tools;

namespace Order.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.RegisterServices();
			services.RegisterPersistant(configuration);
			services.RegisterConfigurations();
		}
		private static void RegisterConfigurations(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(PersistMapperProfile), typeof(ServiceMapper));
			services.AddSingleton<ICustomMapper, CustomMapper>();
		}
		private static void RegisterServices(this IServiceCollection services)
		{
		}
		private static void RegisterPersistant(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContextPool<ApplicationDbContext>(cfg =>
			{
				cfg.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.MigrateDatabase();
		}
		private static void MigrateDatabase(this IServiceCollection services)
		{
			ApplicationDbContext db;
			using var serviceProvider = services.BuildServiceProvider();
			db = serviceProvider.GetService<ApplicationDbContext>();
			if (db?.Database.GetPendingMigrations().Any()??false)
			{
				db.Database.Migrate();
			}
		}
	}
}
