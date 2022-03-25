using Discount.Application.DTOs;
using FluentValidation;

namespace Discount.Configurations.Valiations
{
	public class PercentDiscountValidator : AbstractValidator<PercentDiscountDTO>
	{
		public PercentDiscountValidator()
		{
			RuleFor(i => i.StartDateTime).NotEmpty();
			RuleFor(i => i.EndDateTime).NotEmpty();
			RuleFor(i => i.ProductId).NotEmpty();
			RuleFor(i => i.Percent).GreaterThan(0).LessThan(100).NotEmpty();
			RuleFor(i => i.StoreId).NotNull().NotEmpty();
		}
	}
}