using FileActor.Abstract;
using FileActor.AspNetCore.Abstract;
using FileActor.AspNetCore.Internal;
using FileActor.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileActor.AspNetCore
{
	public static class DIExtensions
	{
		public static IServiceCollection AddFileActor(this IServiceCollection services)
		{
			services.AddSingleton<IFileServiceProvider, FileServiceProvider>();
			services.AddSingleton<IFileStreamerContainer, FileStreamerContainer>();
			services.AddSingleton<FileStreamerFactory>();
			services.AddFileStream<string, Base64FileStream>();
			services.AddAsyncFileStream<string, Base64FileStream>();
			services.AddLocalActor("default", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
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
			services.AddSingleton<IConfigurationManager, AttributeConfigManager>();
			return services;
		}
		public static IServiceCollection AddObjectConfigurations(this IServiceCollection services, Assembly assembly)
		{
			services.AddSingleton<IConfigurationManager, ObjectConfigManager>();
			var definedTypes = assembly.DefinedTypes.Where(i => i.BaseType == typeof(FileActorConfigurable<>));
			Parallel.ForEach(definedTypes, i =>
			{
				services.AddSingleton(typeof(FileActorConfigurable<>).MakeGenericType(i.BaseType?.GenericTypeArguments), i);
			});
			return services;
		}
		public static IServiceCollection AddFileStream<T, TStream>(this IServiceCollection services)
			where TStream : class, IFileStream<T>
		{
			services.AddSingleton<IFileStream<T>, TStream>();
			return services;
		}
		public static IServiceCollection AddAsyncFileStream<T, TStream>(this IServiceCollection services)
			where TStream : class, IAsyncFileStream<T>
		{
			services.AddSingleton<IAsyncFileStream<T>, TStream>();
			return services;
		}
		private static T DiscoverService<T>(this IServiceCollection services)
		{
			using var serviceProvider = services.BuildServiceProvider();
			return serviceProvider.GetService<T>() ?? throw new System.NullReferenceException();
		}
	}
}
