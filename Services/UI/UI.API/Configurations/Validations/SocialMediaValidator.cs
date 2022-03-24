using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UI.Application.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class SocialMediaValidator : AbstractValidator<SocialMediaDTO>, IValidatorInterceptor
	{
		public SocialMediaValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id)
				.NotNull()
				.Must(id =>
				{
					return unitOfWork.SocialMediaRepo.Exists(i => i.Id == id);
				});
			});

			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.ImagePath)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Link)
				.NotEmpty()
				.NotNull();
		}
		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as SocialMediaDTO,
				(opt) =>
				{
					opt.IncludeRuleSets("update-model");
				});
				result.Errors.AddRange(updateModelRes.Errors);
			}
			return result;
		}

		public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
		{
			return commonContext;
		}

	}
}
