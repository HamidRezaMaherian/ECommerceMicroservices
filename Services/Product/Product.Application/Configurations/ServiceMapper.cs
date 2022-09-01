using AutoMapper;
using Product.Application.DTOs;
using Product.Domain.Entities;
using Product.Domain.ValueObjects;

namespace Product.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<BrandDTO, Brand>()
			.ForMember(d => d.Image, cfg =>
				cfg.MapFrom(s => new Blob("", Path.GetDirectoryName(s.ImagePath), Path.GetFileName(s.ImagePath)))
			);
		CreateMap<CategoryPropertyDTO, CategoryProperty>();
		CreateMap<ProductCategoryDTO, ProductCategory>();
		CreateMap<ProductDTO, Domain.Entities.Product>()
			.ForMember(d => d.MainImage, cfg =>
				cfg.MapFrom(s => new Blob("", Path.GetDirectoryName(s.MainImagePath), Path.GetFileName(s.MainImagePath)))
		);
		CreateMap<ProductImageDTO, ProductImage>()
			.ForMember(d => d.Image, cfg =>
				cfg.MapFrom(s => new Blob("", Path.GetDirectoryName(s.ImagePath), Path.GetFileName(s.ImagePath)))
			);
		CreateMap<ProductPropertyDTO, ProductProperty>();
		CreateMap<PropertyDTO, Property>();
	}
}