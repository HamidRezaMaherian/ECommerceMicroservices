using AutoMapper;
using UI.Application.DTOs;
using UI.Domain.Entities;

namespace UI.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<SliderDTO, Slider>().ReverseMap();
		CreateMap<SocialMediaDTO, SocialMedia>().ReverseMap();
		CreateMap<FaqDTO, FAQ>().ReverseMap();
		CreateMap<FaqCategoryDTO, FaqCategory>().ReverseMap();
		CreateMap<AboutUsDTO, AboutUs>().ReverseMap();
		CreateMap<ContactUsDTO, ContactUs>().ReverseMap();
	}
}