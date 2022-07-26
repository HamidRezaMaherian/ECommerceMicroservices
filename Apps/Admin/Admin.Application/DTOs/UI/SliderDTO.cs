namespace Admin.Application.DTOs.UI
{
	public abstract class SliderDTO
	{
		public virtual string Id { get; set; }
		public virtual string Title { get; set; }
		public abstract string Image { get; set; }
	}
}
