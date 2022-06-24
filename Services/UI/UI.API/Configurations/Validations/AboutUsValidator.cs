using FluentValidation;
using UI.API.Configurations.DTOs;

namespace UI.API.Configurations.Validations
{
	public class AboutUsValidator : AbstractValidator<UpdateAboutUsDTO>
	{
		public AboutUsValidator()
		{
			RuleFor(i => i.Title).NotEmpty().NotNull();
			RuleFor(i => i.ShortDesc).NotEmpty().NotNull();
			RuleFor(i => i.Description).MaximumLength(500).NotEmpty().NotNull();
		}
	}
}
