using AutoMapper;
using UI.API.Configurations.DTOs;
using UI.Domain.Entities;

namespace UI.API.Tests.Utils
{
	public class TestMapperProfile : Profile
	{
		public TestMapperProfile()
		{
			CreateMap<UpdateSliderDTO, Slider>().ReverseMap();
			CreateMap<CreateSliderDTO, Slider>().ReverseMap();

			CreateMap<UpdateSocialMediaDTO, SocialMedia>().ReverseMap();
			CreateMap<CreateSocialMediaDTO, SocialMedia>().ReverseMap();

			CreateMap<UpdateFaqDTO, FAQ>().ReverseMap();
			CreateMap<CreateFaqDTO, FAQ>().ReverseMap();

			CreateMap<UpdateFaqCategoryDTO, FaqCategory>().ReverseMap();
			CreateMap<CreateFaqCategoryDTO, FaqCategory>().ReverseMap();

			CreateMap<UpdateAboutUsDTO, AboutUs>().ReverseMap();
			CreateMap<UpdateContactUsDTO, ContactUs>().ReverseMap();
		}
	}
}
