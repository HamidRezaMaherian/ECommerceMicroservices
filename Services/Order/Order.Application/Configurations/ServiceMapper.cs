using AutoMapper;
using Order.Application.DTOs;
using Order.Domain.Entities;

namespace Order.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<BrandDTO, Brand>().ReverseMap();
		CreateMap<CategoryPropertyDTO, CategoryProperty>().ReverseMap();
		CreateMap<ProductCategoryDTO, ProductCategory>().ReverseMap();
		CreateMap<ProductDTO, Domain.Entities.Product>().ReverseMap();
		CreateMap<ProductImageDTO, ProductImage>().ReverseMap();
		CreateMap<ProductPropertyDTO, ProductProperty>().ReverseMap();
		CreateMap<PropertyDTO, Property>().ReverseMap();
	}
}