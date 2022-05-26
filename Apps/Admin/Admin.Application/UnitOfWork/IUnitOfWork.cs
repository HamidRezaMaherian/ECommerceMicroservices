using Admin.Application.Services;

namespace Admin.Application.UnitOfWork
{
	public interface IUnitOfWork
	{
		IUIService UI { get; }
	}
}
