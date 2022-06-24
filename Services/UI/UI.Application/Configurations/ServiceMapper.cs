using AutoMapper;
using UI.Application.DTOs;
using UI.Domain.Entities;

namespace UI.Application.Configurations;

public class ServiceMapper : Profile
{
	public ServiceMapper()
	{
		CreateMap<SliderDTO, Slider>().ForMember(i=>i.ImagePath,
			opt => opt.Condition((source,dest,sourceObj,destObj)=>FileNullCondition(sourceObj, destObj))
			);
		CreateMap<SocialMediaDTO, SocialMedia>().ForMember(i => i.ImagePath, opt => opt.Ignore());
		CreateMap<FaqDTO, FAQ>();
		CreateMap<FaqCategoryDTO, FaqCategory>();
		CreateMap<AboutUsDTO, AboutUs>();
		CreateMap<ContactUsDTO, ContactUs>();
	}
	private bool FileNullCondition(object sourceMember,object destMember)
	{
		if (sourceMember != null)
			return true;
		return false;
	}
}