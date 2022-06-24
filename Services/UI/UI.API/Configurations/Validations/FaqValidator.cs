using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UI.API.Configurations.DTOs;
using UI.Application.DTOs;
using UI.Application.UnitOfWork;

namespace UI.API.Configurations.Validations
{
	public class CreateFaqValidator : AbstractValidator<CreateFaqDTO>
	{
		public CreateFaqValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id).Null().Empty();
			RuleFor(i => i.Question).NotEmpty().NotNull();
			RuleFor(i => i.Answer).NotNull();
			RuleFor(i => i.CategoryId).Must(categoryId =>
			{
				return unitOfWork.FaqCategoryRepo.Exists(i => i.Id == categoryId);
			});
		}
	}
	public class UpdateFaqValidator : AbstractValidator<UpdateFaqDTO>
	{
		public UpdateFaqValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id).NotNull().Must(id =>
			{
				return unitOfWork.FaqRepo.Exists(i => i.Id == id);
			});
			RuleFor(i => i.Question).NotEmpty().NotNull();
			RuleFor(i => i.Answer).NotNull();
			RuleFor(i => i.CategoryId).Must(categoryId =>
			{
				return unitOfWork.FaqCategoryRepo.Exists(i => i.Id == categoryId);
			});
		}
	}
}
