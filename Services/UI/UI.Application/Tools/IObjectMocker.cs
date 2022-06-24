namespace UI.Application.Tools
{
	public interface IObjectMocker<T, Tdto>
	{
		Tdto MockObject(T obj);
	}
}