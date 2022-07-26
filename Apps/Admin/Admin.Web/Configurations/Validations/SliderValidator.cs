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
				RuleFor(i => i.Image).Must(image =>
				{
					if (image != null)
						return Convert.TryFromBase64String(image, new Span<byte>(new byte[image.Length]), out _);
					return true;
				}).WithMessage("value is not a valid base64 string");
			});
			RuleSet(Statics.CREATE_MODEL, () =>
			{
				RuleFor(i => i.Image).NotNull().NotEmpty()
				.Must(image =>
					{
						return Convert.TryFromBase64String(image, new Span<byte>(new byte[image.Length]), out _);
					}).WithMessage("value is not a valid base64 string");
			});
		}
	}
}