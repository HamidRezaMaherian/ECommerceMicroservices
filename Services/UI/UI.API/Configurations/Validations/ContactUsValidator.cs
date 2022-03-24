using FluentValidation;
using UI.Application.DTOs;

namespace UI.API.Configurations.Validations
{
	public class ContactUsValidator : AbstractValidator<ContactUsDTO>
	{
		public ContactUsValidator()
		{
			RuleFor(i => i.Email).NotEmpty().NotNull();
			RuleFor(i => i.Location).NotEmpty().NotNull();
			RuleFor(i => i.Address).NotEmpty().NotNull();
			RuleFor(i => i.PhoneNumber).NotEmpty().NotNull();
			//RuleFor(i => i.Lat).NotEmpty().NotNull();
			//RuleFor(i => i.Lng).NotEmpty().NotNull();
		}
	}
}
