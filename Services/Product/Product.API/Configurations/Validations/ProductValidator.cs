using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Product.Application.DTOs;

namespace Product.API.Configurations.Validations
{
	public class ProductValidator : AbstractValidator<ProductDTO>
	{
		public ProductValidator()
		{
			RuleFor(i => i.Name).NotEmpty().NotNull();
			RuleFor(i => i.CategoryId).NotEmpty().NotNull();
			RuleFor(i => i.CreatedDateTime).NotEmpty().NotNull();
			RuleFor(i => i.UnitPrice).NotEmpty().NotNull();
		}
	}
}
