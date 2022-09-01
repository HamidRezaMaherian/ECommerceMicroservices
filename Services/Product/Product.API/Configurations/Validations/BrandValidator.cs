using FluentValidation;
using Product.API.Configurations.DTOs;
using Product.Application.UnitOfWork;

namespace Product.API.Configurations.Validations
{
	public class CreateBrandValidator : AbstractValidator<CreateBrandDTO>
	{
		public CreateBrandValidator()
		{
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.Image)
				.NotEmpty()
				.NotNull();
		}

	}
	public class UpdateBrandValidator : AbstractValidator<UpdateBrandDTO>
	{
		public UpdateBrandValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id)
			.NotNull()
			.Must(id =>
			{
				return unitOfWork.BrandRepo.Exists(i => i.Id == id);
			});
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.ImagePath)
				.Null()
				.Empty();
		}
	}
}
