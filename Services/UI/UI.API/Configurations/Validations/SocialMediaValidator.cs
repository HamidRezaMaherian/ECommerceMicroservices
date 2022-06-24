using FluentValidation;
using UI.API.Configurations.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class CreateSocialMediaValidator : AbstractValidator<CreateSocialMediaDTO>
	{
		public CreateSocialMediaValidator()
		{
			RuleFor(i => i.Id).Null().Empty();
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Image)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Link)
				.NotEmpty()
				.NotNull();
		}
	}
	public class UpdateSocialMediaValidator : AbstractValidator<UpdateSocialMediaDTO>
	{
		public UpdateSocialMediaValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id)
			.NotNull()
			.Must(id =>
			{
				return unitOfWork.SocialMediaRepo.Exists(i => i.Id == id);
			});
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.ImagePath)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Link)
				.NotEmpty()
				.NotNull();
		}
	}
}
