using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Inventory.Application.DTOs;
using Inventory.Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Services.Shared.Resources;

namespace Inventory.API.Configurations.Validations
{
	public class StockValidator : AbstractValidator<StockDTO>, IValidatorInterceptor
	{
		public StockValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id).NotNull().Must(id =>
				{
					return unitOfWork.StockRepo.Exists(i => i.Id == id);
				});
			});
			RuleFor(i => i.ProductId).NotEmpty().NotNull();
			RuleFor(i => i.Count).NotEmpty().NotNull();
			RuleFor(i => i.StoreId)
				.Length(24,24)
				.Must(storeId =>
				{
					return unitOfWork.StoreRepo.Exists(i => i.Id == storeId);
				}).WithMessage(obj => string.Format(Messages.NOT_FOUND, nameof(obj.StoreId)));
		}

		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as StockDTO,
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
