using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UI.Application.Configurations;
using UI.Application.DTOs;
using UI.Application.Services;
using UI.Application.Tools;
using UI.Application.UnitOfWork;
using UI.Domain.Entities;
using UI.Infrastructure.Persist;
using UI.Infrastructure.Persist.Mappings;
using UI.Infrastructure.Services;
using UI.Infrastructure.Tools;

namespace UI.Infrastructure.IOC
{
	public static class IocRegistery
	{
		public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
		{
			services.RegisterServices();
			services.RegisterPersistant();
			services.RegisterConfigurations();
			return services;
		}
		public static void AddCdnResolver<T>(this IServiceCollection services) where T:class,ICdnResolver
		{
			services.AddSingleton<ICdnResolver,T>();
		}

		private static void RegisterConfigurations(this IServiceCollection services)
		{
			services.AddSingleton<ServiceMapper>();
			services.AddSingleton<PersistMapperProfile>();
			services.AddSingleton(provider =>
			{
				return new MapperConfiguration(cfg =>
				{
					cfg.AddProfile(provider.GetService<ServiceMapper>());
					cfg.AddProfile(provider.GetService<PersistMapperProfile>());
				}).CreateMapper();
			});
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
			services.AddScoped(provider =>
			{
				var mongoClient = provider.GetService<MongoClient>();
				return new ApplicationDbContext(mongoClient, "UiDb");
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
