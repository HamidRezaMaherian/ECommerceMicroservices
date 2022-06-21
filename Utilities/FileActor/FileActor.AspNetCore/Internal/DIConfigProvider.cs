using FileActor.Abstract.Factory;
using FileActor.Exceptions;
using FileActor.Internal;
using System;

namespace FileActor.AspNetCore.Internal
{
	public class DIConfigProvider : IConfigProvider
	{
		private readonly IServiceProvider _serviceProvider;
		public DIConfigProvider(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public IFileActorConfigurable ProvideConfiguration(Type type)
		{
			return 
				(IFileActorConfigurable)_serviceProvider.GetService(typeof(FileActorConfigurable<>).MakeGenericType(type))
				?? (IFileActorConfigurable)_serviceProvider.GetService(typeof(FileActorConfigurable<>).MakeGenericType(type.BaseType))
				?? throw new NotFoundException($"configuration for {type} not found");
		}
	}
}
