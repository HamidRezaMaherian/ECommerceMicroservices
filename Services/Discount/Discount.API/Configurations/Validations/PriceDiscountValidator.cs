using Discount.Application.DTOs;
using FluentValidation;

namespace Discount.Configurations.Validations
{
	public class PriceDiscountValidator : AbstractValidator<PriceDiscountDTO>
	{
		public PriceDiscountValidator()
		{
			RuleFor(i => i.StartDateTime).NotEmpty();
			RuleFor(i => i.EndDateTime).NotEmpty();
			RuleFor(i => i.Price).NotEmpty();
			RuleFor(i => i.ProductId).NotEmpty();
			RuleFor(i => i.StoreId).NotEmpty();
		}
	}
}