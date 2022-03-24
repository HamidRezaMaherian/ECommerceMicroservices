using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UI.Application.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class AboutUsValidator : AbstractValidator<AboutUsDTO>
	{
		public AboutUsValidator()
		{
			RuleFor(i => i.Title).NotEmpty().NotNull();
			RuleFor(i => i.ShortDesc).NotEmpty().NotNull();
			RuleFor(i => i.Description).MaximumLength(500).NotEmpty().NotNull();
			//RuleFor(i => i.ImagePath).NotEmpty().NotNull();
		}
	}
}
