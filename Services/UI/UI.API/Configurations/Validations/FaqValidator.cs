using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UI.Application.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class FaqValidator : AbstractValidator<FaqDTO>, IValidatorInterceptor
	{
		public FaqValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id).NotNull().Must(id =>
				{
					return unitOfWork.FaqRepo.Exists(i => i.Id == id);
				});
			});
			RuleFor(i => i.Question).NotEmpty().NotNull();
			RuleFor(i => i.Answer).NotNull();
			RuleFor(i => i.CategoryId).Must(categoryId =>
			{
				return unitOfWork.FaqCategoryRepo.Exists(i => i.Id == categoryId);
			});
		}
		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as FaqDTO,
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
