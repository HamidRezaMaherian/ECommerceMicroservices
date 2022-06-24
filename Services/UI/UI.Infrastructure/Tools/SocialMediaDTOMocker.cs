using Moq;
using UI.Application.DTOs;
using UI.Application.Tools;
using UI.Domain.Entities;

namespace UI.Infrastructure.Tools
{
	public class SocialMediaDTOMocker : IObjectMocker<SocialMedia, SocialMediaDTO>
	{
		public SocialMediaDTO MockObject(SocialMedia obj)
		{
			var moq = new Mock<SocialMediaDTO>();
			moq.Setup(i => i.ImagePath).Returns(obj.ImagePath);
			moq.Setup(i => i.Id).Returns(obj.Id);
			moq.Setup(i => i.Name).Returns(obj.Name);
			return moq.Object;
		}
	}
}
