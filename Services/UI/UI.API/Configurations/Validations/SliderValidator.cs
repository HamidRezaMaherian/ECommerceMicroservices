using FluentValidation;
using UI.API.Configurations.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class CreateSliderValidator : AbstractValidator<CreateSliderDTO>
	{
		public CreateSliderValidator()
		{
			RuleFor(i => i.Title)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Image)
				.NotEmpty()
				.NotNull();
		}

	}
	public class UpdateSliderValidator : AbstractValidator<UpdateSliderDTO>
	{
		public UpdateSliderValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id)
			.NotNull()
			.Must(id =>
			{
				return unitOfWork.SliderRepo.Exists(i => i.Id == id);
			});
			RuleFor(i => i.Title)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.ImagePath)
				.Null()
				.Empty();
		}
	}
}
