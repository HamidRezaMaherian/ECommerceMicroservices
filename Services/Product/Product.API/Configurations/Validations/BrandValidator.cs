using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.UnitOfWork;

namespace Product.API.Configurations.Validations
{
	public class BrandValidator : AbstractValidator<BrandDTO>, IValidatorInterceptor
	{
		public BrandValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id).NotNull().Must(id =>
				{
					return unitOfWork.BrandRepo.Exists(i => i.Id == id);
				});
			});
			RuleFor(i => i.Name).NotEmpty().NotNull();
			RuleFor(i => i.ImagePath).NotNull();

		}
		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as BrandDTO,
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
