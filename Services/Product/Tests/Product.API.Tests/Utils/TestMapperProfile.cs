using AutoMapper;
using Product.API.Tests.Utils.HelperTypes;
using Product.Domain.ValueObjects;

namespace Product.API.Tests.Utils;

public class TestMapperProfile : Profile
{
	public TestMapperProfile()
	{
		CreateMap<HelperProductDTO, Domain.Entities.Product>().ForMember(d => d.MainImage, opt => opt.MapFrom(s => new Blob("", s.MainImagePath, "")))
			.ReverseMap();
	}
}

