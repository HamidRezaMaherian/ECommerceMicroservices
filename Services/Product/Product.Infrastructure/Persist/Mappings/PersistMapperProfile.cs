using AutoMapper;
using Product.Domain.Entities;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile()
		{
			CreateMap<ProductDAO, Domain.Entities.Product>().ReverseMap();
			CreateMap<BrandDAO, Brand>().ReverseMap();
			CreateMap<CategoryPropertyDAO, CategoryProperty>().ReverseMap();
			CreateMap<ProductCategoryDAO, ProductCategory>().ReverseMap();
			CreateMap<ProductImageDAO, ProductImage>().ReverseMap();
			CreateMap<ProductPropertyDAO, ProductProperty>().ReverseMap();
			CreateMap<PropertyDAO, Property>().ReverseMap();
		}
	}
}
