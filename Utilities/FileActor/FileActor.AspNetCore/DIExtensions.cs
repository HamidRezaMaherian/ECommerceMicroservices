using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.AspNetCore.Internal;
using FileActor.FileServices;
using FileActor.Internal;
using FileActor.Internal.Factory;
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
			services.AddFileStream();
			services.AddFileExtension();
			services.AddScoped<IFileServiceProvider, FileServiceProvider>();
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
			var fileStreamerFactory = services.DiscoverService<IFileStreamFactory>();
			fileStreamerContainer.Insert(name, new LocalFileStreamer(rootPath, fileStreamerFactory));
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
		public static IServiceCollection AddFileStream(this IServiceCollection services)
		{
			services.AddScoped<IFileStreamFactory>((services) =>
			{
				var streamFactory = new FileStreamFactory();
				streamFactory.Add(typeof(string), () => new Base64FileStream());
				streamFactory.Add(typeof(IFileStream), () => new FormFileStream());
				return streamFactory;
			});
			return services;
		}
		private static IServiceCollection AddFileExtension(this IServiceCollection services)
		{
			services.AddScoped<IFileExtensionFactory>((services) =>
			{
				var streamFactory = new FileExtensionFactory();
				streamFactory.Add(typeof(string), () => new Base64FileExtension());
				streamFactory.Add(typeof(IFileExtension), () => new FormFileExtension());
				return streamFactory;
			});
			return services;
		}
		private static T DiscoverService<T>(this IServiceCollection services)
		{
			using var serviceProvider = services.BuildServiceProvider();
			return serviceProvider.GetService<T>();
		}
	}
}
