using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.UnitOfWork;
using Services.Shared.Resources;

namespace Product.API.Configurations.Validations
{
	public class ProductValidator : AbstractValidator<ProductDTO>, IValidatorInterceptor
	{
		public ProductValidator(IUnitOfWork unitOfWork)
		{
			RuleSet("update-model", () =>
			{
				RuleFor(i => i.Id)
				.NotNull()
				.Must(id =>
				{
					return unitOfWork.ProductRepo.Exists(i => i.Id == id);
				});
			});

			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();

			RuleFor(i => i.CategoryId)
				.Must(categoryId =>
				{
					return unitOfWork.ProductCategoryRepo.Exists(i => i.Id == categoryId);
				}).WithMessage(obj => string.Format(Messages.NOT_FOUND, nameof(obj.CategoryId)))
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.CreatedDateTime)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.UnitPrice)
				.NotEmpty()
				.NotNull();
		}
		public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
		{
			if (actionContext.HttpContext.Request.Method.ToLower() == HttpMethod.Put.Method.ToLower())
			{
				var updateModelRes = this.Validate(validationContext.InstanceToValidate as ProductDTO,
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
