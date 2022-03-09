using Discount.Domain.Entities;
using FluentValidation;

namespace Discount.Configurations.Validations
{
	public class PriceDiscountValidator : AbstractValidator<PriceDiscountDTO>
	{
		public PriceDiscountValidator()
		{
			RuleFor(i => i.StartDateTime).NotEmpty();
			RuleFor(i => i.EndDateTime).NotEmpty();			
		}
	}
}