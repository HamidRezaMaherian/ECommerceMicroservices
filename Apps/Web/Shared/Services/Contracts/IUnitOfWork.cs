using WebApp.Shared.Models.UI;

namespace WebApp.Shared.Services.Contracts
{
	public interface IUnitOfWork
	{
		IUIService UI { get; }
	}
}
