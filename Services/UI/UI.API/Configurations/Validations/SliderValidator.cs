using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UI.API.Configurations.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class SliderValidator : AbstractValidator<SliderDTO>, IValidatorInterceptor
	{
		public SliderValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id)
				.NotNull()
				.Must(id =>
				{
					return unitOfWork.SliderRepo.Exists(i => i.Id == id);
				});
			});

			RuleFor(i => i.Title)
				.NotEmpty()
				.NotNull();
		}
		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as SliderDTO,
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
