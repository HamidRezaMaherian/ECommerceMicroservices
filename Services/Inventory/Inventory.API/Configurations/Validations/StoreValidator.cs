using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Inventory.Application.DTOs;
using Inventory.Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;

namespace Inventory.API.Configurations.Validations
{
	public class StoreValidator : AbstractValidator<StoreDTO>, IValidatorInterceptor
	{
		public StoreValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id)
				.NotNull()
				.Must(id =>
				{
					return unitOfWork.StoreRepo.Exists(i => i.Id == id);
				});
			});

			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.ShortDesc)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Description)
				.NotEmpty()
				.NotNull();
		}
		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as StoreDTO,
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
