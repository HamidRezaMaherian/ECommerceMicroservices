using AutoMapper;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
	public class CategoryPropertyService : GenericBaseService<Domain.Entities.CategoryProperty, CategoryPropertyDTO>, ICategoryPropertyService
	{
		public CategoryPropertyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}
	}
}
