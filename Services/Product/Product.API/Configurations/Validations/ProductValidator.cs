using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Product.API.Configurations.DTOs;
using Product.Application.DTOs;
using Product.Application.UnitOfWork;
using Services.Shared.Resources;

namespace Product.API.Configurations.Validations
{
	public class CreateProductValidator : AbstractValidator<CreateProductDTO>
	{
		public CreateProductValidator()
		{
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.MainImage)
				.NotEmpty()
				.NotNull();
		}

	}
	public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
	{
		public UpdateProductValidator(IUnitOfWork unitOfWork)
		{
			RuleFor(i => i.Id)
			.NotNull()
			.Must(id =>
			{
				return unitOfWork.ProductRepo.Exists(i => i.Id == id);
			});
			RuleFor(i => i.Name)
				.NotEmpty()
				.NotNull();
			RuleFor(i => i.MainImagePath)
				.Null()
				.Empty();
		}
	}
}
