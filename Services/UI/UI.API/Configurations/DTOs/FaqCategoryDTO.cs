using System.Text.Json.Serialization;

namespace UI.API.Configurations.DTOs
{
	public class CreateFaqCategoryDTO : Application.DTOs.FaqCategoryDTO
	{
		[JsonIgnore]
		public override string Id { get => base.Id; set => base.Id = value; }
	}
	public class UpdateFaqCategoryDTO : Application.DTOs.FaqCategoryDTO
	{
	}
}
