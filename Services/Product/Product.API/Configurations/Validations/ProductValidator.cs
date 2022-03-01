using FluentValidation;
using Product.Application.DTOs;

namespace Product.API.Configurations.Validations
{
	public class ProductValidator:AbstractValidator<ProductDTO>
	{
		public ProductValidator()
		{
			RuleFor(i => i.Name).NotEmpty().NotNull();
			RuleFor(i => i.Name).NotEmpty().NotNull();
		}
	}
}
