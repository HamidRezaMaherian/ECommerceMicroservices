using AutoMapper;
using UI.Application.DTOs;
using UI.Domain.Entities;

namespace UI.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<SliderDTO, Slider>().ForMember(i=>i.ImagePath,opt=>opt.Ignore());
		CreateMap<SocialMediaDTO, SocialMedia>().ForMember(i => i.ImagePath, opt => opt.Ignore());
		CreateMap<FaqDTO, FAQ>();
		CreateMap<FaqCategoryDTO, FaqCategory>();
		CreateMap<AboutUsDTO, AboutUs>();
		CreateMap<ContactUsDTO, ContactUs>();
	}
}