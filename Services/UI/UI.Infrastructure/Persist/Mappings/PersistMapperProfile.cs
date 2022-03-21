using AutoMapper;
using UI.Domain.Entities;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile()
		{
			CreateMap<SliderDAO, Slider>().ReverseMap();
			CreateMap<AboutUsDAO, AboutUs>().ReverseMap();
			CreateMap<ContactUsDAO, ContactUs>().ReverseMap();
			CreateMap<FaqDAO, FAQ>().ReverseMap();
			CreateMap<FaqCategoryDAO, FaqCategory>().ReverseMap();
			CreateMap<SocialMediaDAO, SocialMedia>().ReverseMap();
		}
	}
}
