using System.Text.Json.Serialization;

namespace UI.API.Configurations.DTOs
{
	public class CreateFaqDTO : Application.DTOs.FaqDTO
	{
		[JsonIgnore]
		public override string Id { get => base.Id; set => base.Id = value; }
	}
	public class UpdateFaqDTO : Application.DTOs.FaqDTO
	{
	}
}
