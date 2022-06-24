namespace UI.Application.Tools
{
	public interface IObjectMocker
	{
		T MockObject<T>() where T:class;
	}
}