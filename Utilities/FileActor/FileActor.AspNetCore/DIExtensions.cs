using FileActor.Abstract;
using FileActor.AspNetCore.Abstract;
using FileActor.AspNetCore.Internal;
using FileActor.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileActor.AspNetCore
{
	public static class DIExtensions
	{
		public static IServiceCollection AddFileActor(this IServiceCollection services)
		{
			services.AddFileStream<string, Base64FileStream>();
			services.AddFileStream<IFormFile, FormFileStream>();
			services.AddScoped<IFileServiceProvider, FileServiceProvider>();
			services.AddSingleton(new FileStreamerFactory(services.BuildServiceProvider()));
			return services;
		}
		public static IServiceCollection AddInMemoryContainer(this IServiceCollection services)
		{
			services.AddSingleton(Statics.FileStreamerContainer);
			return services;
		}
		public static IServiceCollection AddLocalActor(this IServiceCollection services, string name, string rootPath)
		{
			var fileStreamerContainer = services.DiscoverService<IFileStreamerContainer>();
			var fileStreamerFactory = services.DiscoverService<FileStreamerFactory>();
			fileStreamerContainer.Insert(name, fileStreamerFactory.CreateLocalFileStream(rootPath));
			return services;
		}
		public static IServiceCollection AddAttributeConfiguration(this IServiceCollection services)
		{
			services.AddScoped<IConfigurationManager, AttributeConfigManager>();
			return services;
		}
		public static IServiceCollection AddObjectConfigurations(this IServiceCollection services, Assembly assembly)
		{
			services.AddScoped<IConfigurationManager>(sp => new ObjectConfigManager(services.BuildServiceProvider()));
			var definedTypes = assembly.DefinedTypes.Where(i => i.BaseType == typeof(FileActorConfigurable<>));
			Parallel.ForEach(definedTypes, i =>
			{
				services.AddScoped(typeof(FileActorConfigurable<>).MakeGenericType(i.BaseType?.GenericTypeArguments ?? throw new NullReferenceException()), i);
			});
			return services;
		}
		public static IServiceCollection AddFileStream<T, TStream>(this IServiceCollection services)
			where T:class
			where TStream : FileStream<T>
		{
			services.AddScoped<FileStream<T>, TStream>();
			return services;
		}
		private static T DiscoverService<T>(this IServiceCollection services)
		{
			using var serviceProvider = services.BuildServiceProvider();
			return serviceProvider.GetService<T>();
		}
	}
}
