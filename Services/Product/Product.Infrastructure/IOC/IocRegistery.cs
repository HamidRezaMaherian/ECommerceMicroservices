using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Configurations;
using Product.Application.Services;
using Product.Application.Tools;
using Product.Application.UnitOfWork;
using Product.Infrastructure.Persist;
using Product.Infrastructure.Persist.Mappings;
using Product.Infrastructure.Services;
using Product.Infrastructure.Tools;

namespace Product.Infrastructure.IOC
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
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductCategoryService, ProductCategoryService>();
			services.AddScoped<IBrandService, BrandService>();
		}
		private static void RegisterPersistant(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContextPool<ApplicationDbContext>(cfg =>
			{
				cfg.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
