using Admin.Web.ViewModels;
using FluentValidation;

namespace Admin.Web.Configurations.Valiations
{
	public class SliderValidator : AbstractValidator<SliderVM>
	{
		public SliderValidator()
		{
			RuleFor(i => i.Title).NotNull().NotEmpty();
			RuleSet(Statics.UPDATE_MODEL, () =>
			{
				RuleFor(i => i.Id).NotNull().NotEmpty();
			});
			RuleSet(Statics.CREATE_MODEL, () =>
			{
				RuleFor(i => i.Image).NotNull().NotEmpty();
			});
		}
	}
}