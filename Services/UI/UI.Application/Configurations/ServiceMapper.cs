using AutoMapper;
using UI.Application.DTOs;
using UI.Domain.Entities;
using UI.Domain.ValueObjects;

namespace UI.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<SliderDTO, Slider>().ForMember(d => d.Image, cfg =>
			cfg.MapFrom(s => new Blob("", Path.GetDirectoryName(s.ImagePath), Path.GetFileName(s.ImagePath)))
		);
		CreateMap<SocialMediaDTO, SocialMedia>()
			.ForMember(d => d.Image, cfg =>
			cfg.MapFrom(s => new Blob("", Path.GetDirectoryName(s.ImagePath), Path.GetFileName(s.ImagePath)))
		);
		CreateMap<FaqDTO, FAQ>();
		CreateMap<FaqCategoryDTO, FaqCategory>();
		CreateMap<AboutUsDTO, AboutUs>();
		CreateMap<ContactUsDTO, ContactUs>();
	}
}