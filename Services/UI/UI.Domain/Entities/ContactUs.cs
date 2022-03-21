using Services.Shared.Common;

namespace UI.Domain.Entities;
public class ContactUs : EntityPrimaryBase<string>
{
	public string Address { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public string Location { get; set; }
	public string Lat { get; set; }
	public string Lng { get; set; }
}
