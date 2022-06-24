using Moq;
using UI.Application.DTOs;
using UI.Application.Tools;
using UI.Domain.Entities;

namespace UI.Infrastructure.Tools
{
	public class SliderDTOMocker : IObjectMocker<Slider, SliderDTO>
	{
		public SliderDTO MockObject(Slider obj)
		{
			var moq = new Mock<SliderDTO>();
			moq.Setup(i => i.ImagePath).Returns(obj.ImagePath);
			moq.Setup(i => i.Id).Returns(obj.Id);
			moq.Setup(i => i.Title).Returns(obj.Title);
			return moq.Object;
		}
	}
}
