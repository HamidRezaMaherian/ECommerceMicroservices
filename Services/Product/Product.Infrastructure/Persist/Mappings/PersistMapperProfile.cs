using AutoMapper;
using Product.Application.Tools;
using Product.Domain.Entities;
using Product.Domain.ValueObjects;
using Product.Infrastructure.Persist.DAOs;

namespace Product.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile(ICdnResolver cdnResolver)
		{
			CreateMap<ProductDAO, Domain.Entities.Product>()
				.ForMember(d => d.MainImage, cfg =>
					cfg.MapFrom(s => new Blob(cdnResolver.GetAddress(), (Path.GetDirectoryName(s.MainImagePath) ?? "").Trim('/', '\\'), Path.GetFileName(s.MainImagePath)))
				)
				.ReverseMap()
				.ForMember(d => d.MainImagePath, cfg =>
					cfg.MapFrom(s => s.MainImage.ToString()??null)
				);

			CreateMap<BrandDAO, Brand>()
				.ForMember(d => d.Image, cfg =>
					cfg.MapFrom(s => new Blob(cdnResolver.GetAddress(), (Path.GetDirectoryName(s.ImagePath) ?? "").Trim('/', '\\'), Path.GetFileName(s.ImagePath)))
				)
				.ReverseMap()
				.ForMember(d => d.ImagePath, cfg =>
					cfg.MapFrom(s => s.Image.ToString() ?? null)
				);
			;
			CreateMap<CategoryPropertyDAO, CategoryProperty>().ReverseMap();
			CreateMap<ProductCategoryDAO, ProductCategory>().ReverseMap();
			CreateMap<ProductImageDAO, ProductImage>()
				.ForMember(d => d.Image, cfg =>
					cfg.MapFrom(s => new Blob(cdnResolver.GetAddress(), (Path.GetDirectoryName(s.ImagePath) ?? "").Trim('/', '\\'), Path.GetFileName(s.ImagePath)))
				)
				.ReverseMap()
				.ForMember(d => d.ImagePath, cfg =>
					cfg.MapFrom(s => s.Image.ToString() ?? null)
				);
			;
			CreateMap<ProductPropertyDAO, ProductProperty>().ReverseMap();
			CreateMap<ProductPricePropertyDAO, ProductPriceProperty>().ReverseMap();
			CreateMap<PropertyDAO, Property>().ReverseMap();
		}
	}
}
