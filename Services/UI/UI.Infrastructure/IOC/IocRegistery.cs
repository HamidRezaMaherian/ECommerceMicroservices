using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Services.Shared.Contracts;
using Services.Shared.Mapper;
using UI.Application.Configurations;
using UI.Application.Services;
using UI.Application.UnitOfWork;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.Mappings;
using UI.Infrastructure.Services;

namespace UI.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static void RegisterInfrastructure(this IServiceCollection services)
		{
			services.RegisterServices();
			services.RegisterPersistant();
			services.RegisterConfigurations();
		}
		private static void RegisterConfigurations(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(PersistMapperProfile), typeof(ServiceMapper));
			services.AddSingleton<ICustomMapper, CustomMapper>();
		}

		private static void RegisterServices(this IServiceCollection services)
		{
			services.AddScoped<IFaqService, FaqService>();
			services.AddScoped<IFaqCategoryService, FaqCategoryService>();
			services.AddScoped<ISocialMediaService, SocialMediaService>();
			services.AddScoped<ISliderService, SliderService>();
			services.AddScoped<IAboutUsService, AboutUsService>();
			services.AddScoped<IContactUsService, ContactUsService>();
		}
		private static void RegisterPersistant(this IServiceCollection services)
		{
			services.AddScoped<ApplicationDbContext>(provider =>
			{
				var mongoClient = provider.GetService<MongoClient>();
				return new ApplicationDbContext(mongoClient, "ui-db");
			});
			services.AddScoped(provider =>
			{
				var configuration = provider.GetService<IConfiguration>();
				return new MongoClient(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
