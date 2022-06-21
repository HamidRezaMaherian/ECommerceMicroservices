using FileActor.Abstract;
using FileActor.Abstract.Factory;
using FileActor.AspNetCore.Internal;
using FileActor.FileServices;
using FileActor.Internal;
using FileActor.Internal.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileActor.AspNetCore
{
	public static class DIExtensions
	{
		/// <summary>
		/// add FileActor base services
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddFileActor(this IServiceCollection services)
		{
			services.AddFileTypeHelper();
			services.AddScoped<IFileServiceActor, FileServiceActor>();
			return services;
		}
		/// <summary>
		/// adds inMemory container for file streamers
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddInMemoryContainer(this IServiceCollection services)
		{
			services.AddSingleton(Statics.FileStreamerContainer);
			return services;
		}
		/// <summary>
		/// insert local fileStreamer into container
		/// </summary>
		/// <param name="services"></param>
		/// <param name="name">unique name</param>
		/// <param name="rootPath">rootpath wich will be joined with configuration paths in objects</param>
		/// <returns></returns>
		public static IServiceCollection AddLocalActor(this IServiceCollection services, string name, string rootPath)
		{
			var fileStreamerContainer = services.DiscoverService<IFileStreamerContainer>();
			var fileStreamerFactory = services.DiscoverService<IFileTypeHelperProvider>();
			fileStreamerContainer.Insert(name, new LocalFileStreamer(rootPath, fileStreamerFactory));
			return services;
		}
		/// <summary>
		/// uses attributes for configuration manager
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddAttributeConfiguration(this IServiceCollection services)
		{
			services.AddScoped<IConfigurationManager, AttributeConfigManager>();
			return services;
		}
		/// <summary>
		/// searches given assmebly for implemented objectConfigurables
		/// </summary>
		/// <param name="services"></param>
		/// <param name="assembly">target assembly</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"></exception>
		public static IServiceCollection AddObjectConfigurations(this IServiceCollection services, Assembly assembly)
		{
			var definedTypes = assembly.DefinedTypes.Where(i => i.BaseType?.Name == typeof(FileActorConfigurable<>).Name);
			Parallel.ForEach(definedTypes, i =>
			{
				services.AddScoped(typeof(FileActorConfigurable<>).MakeGenericType(i.BaseType?.GenericTypeArguments ?? throw new NullReferenceException()), i);
			});
			services.AddScoped<IConfigProvider>((sp)=>
			{
				return new DIConfigProvider(sp);
			});
			services.AddScoped<IConfigurationManager,ObjectConfigManager>();
			return services;
		}
		private static IServiceCollection AddFileTypeHelper(this IServiceCollection services)
		{
			services.AddScoped<IFileTypeHelperProvider>((services) =>
			{
				var streamFactory = new FileStreamProvider();
				streamFactory.Add(typeof(string), () => new Base64FileHelper());
				streamFactory.Add(typeof(IFileTypeHelper), () => new FormFileStream());
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
