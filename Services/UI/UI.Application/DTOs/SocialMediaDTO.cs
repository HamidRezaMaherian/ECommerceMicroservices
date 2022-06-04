namespace UI.Application.DTOs;
public abstract class SocialMediaDTO
{
	public virtual string Id { get; set; }
	public virtual string Name { get; set; }
	public virtual string Link { get; set; }
	public virtual string ImagePath { get; set; }
	public virtual bool IsActive { get; set; }
}