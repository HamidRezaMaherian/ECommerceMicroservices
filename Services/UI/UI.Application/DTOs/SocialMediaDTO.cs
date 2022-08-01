using UI.Domain.Entities;

namespace UI.Application.DTOs;
public abstract class SocialMediaDTO : BaseDTO<string>
{
	public virtual string Name { get; set; }
	public virtual string Link { get; set; }
	public virtual string ImagePath { get; set; }
	public virtual bool IsActive { get; set; }
	public abstract object GetImage();
}