﻿using AutoMapper;
using System.Linq.Expressions;
using UI.Application.Tools;
using UI.Domain.Entities;
using UI.Domain.ValueObjects;
using UI.Infrastructure.Persist.DAOs;

namespace UI.Infrastructure.Persist.Mappings
{
	public class PersistMapperProfile : Profile
	{
		public PersistMapperProfile(ICdnResolver cdnResolver)
		{

			CreateMap<SliderDAO, Slider>()
				.ForMember(d => d.Image, cfg =>
					cfg.MapFrom(s => new Blob(cdnResolver.GetAddress(), Path.GetDirectoryName(s.ImagePath).Trim('/','\\'), Path.GetFileName(s.ImagePath)))
				)
				.ReverseMap()
				.ForMember(d => d.ImagePath, cfg =>
					cfg.MapFrom(s => s.Image.ToString())
					);
			CreateMap<AboutUsDAO, AboutUs>().ReverseMap();
			CreateMap<ContactUsDAO, ContactUs>().ReverseMap();
			CreateMap<FaqDAO, FAQ>().ReverseMap();
			CreateMap<FaqCategoryDAO, FaqCategory>().ReverseMap();

			CreateMap<SocialMediaDAO, SocialMedia>()
				.ForMember(d => d.Image, cfg =>
					cfg.MapFrom(s => new Blob(cdnResolver.GetAddress(), Path.GetDirectoryName(s.ImagePath), Path.GetFileName(s.ImagePath)))
				)
				.ReverseMap()
				.ForMember(d => d.ImagePath, cfg =>
					cfg.MapFrom(s => s.Image.ToString())
				);
		}
	}
}
