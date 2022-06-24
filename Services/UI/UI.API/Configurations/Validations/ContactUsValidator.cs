using FluentValidation;
using UI.API.Configurations.DTOs;

namespace UI.API.Configurations.Validations
{
	public class ContactUsValidator : AbstractValidator<UpdateContactUsDTO>
	{
		public ContactUsValidator()
		{
			RuleFor(i => i.Email).NotEmpty().NotNull();
			RuleFor(i => i.Location).NotEmpty().NotNull();
			RuleFor(i => i.Address).NotEmpty().NotNull();
			RuleFor(i => i.PhoneNumber).NotEmpty().NotNull();
		}
	}
}
