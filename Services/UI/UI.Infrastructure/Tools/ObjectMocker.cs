using Moq;
using UI.Application.Tools;

namespace UI.Infrastructure.Tools
{
	public class ObjectMocker : IObjectMocker
	{
		public T MockObject<T>() where T : class
		{
			var moq = new Mock<T>();
			return moq.Object;
		}
	}
}
