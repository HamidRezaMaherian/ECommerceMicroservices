using FileActor.Internal;
using System;

namespace FileActor.Abstract.Factory
{
	/// <summary>
	/// provider for related configurations
	/// </summary>
	public interface IConfigProvider
	{
		/// <summary>
		/// provides objectConfiguration base on the given type
		/// </summary>
		/// <param name="type">typeof configuration</param>
		IFileActorConfigurable ProvideConfiguration(Type type);
	}
}
