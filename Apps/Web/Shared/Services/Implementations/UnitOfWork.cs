using Microsoft.Extensions.DependencyInjection;
using WebApp.Shared.Models;

namespace WebApp.Shared.Services.Contracts
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IServiceProvider _serviceProvider;

		public UnitOfWork(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public IUIService UI =>_serviceProvider.GetService<IUIService>();
	}
}
