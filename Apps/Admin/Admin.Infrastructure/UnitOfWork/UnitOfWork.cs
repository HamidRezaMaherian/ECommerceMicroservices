
using Admin.Application.Services;
using Admin.Application.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Infrastructure.UnitOfWork
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
