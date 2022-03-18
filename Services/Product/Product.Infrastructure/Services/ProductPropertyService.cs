﻿using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;
using Services.Shared.Contracts;

namespace Product.Infrastructure.Services
{
	public class ProductPropertyService : GenericBaseService<Domain.Entities.ProductProperty, ProductPropertyDTO>, IProductPropertyService
	{
		public ProductPropertyService(IUnitOfWork unitOfWork, ICustomMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
