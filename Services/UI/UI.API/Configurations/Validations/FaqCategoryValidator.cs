using FluentValidation;
using UI.API.Configurations.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class CreateFaqCategoryValidator : AbstractValidator<CreateFaqCategoryDTO>
	{
		public CreateFaqCategoryValidator()
		{
			RuleFor(i => i.Id).Null().Empty();
			RuleFor(i => i.Name).NotEmpty().NotNull();
		}
	}
	public class UpdateFaqCategoryValidator : AbstractValidator<UpdateFaqCategoryDTO>
	{
		public UpdateFaqCategoryValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id).NotNull().Must(id =>
			{
				return unitOfWork.FaqCategoryRepo.Exists(i => i.Id == id);
			});
			RuleFor(i => i.Name).NotEmpty().NotNull();
		}
	}
}
