using AutoMapper;
using Product.Domain.ValueObjects;
using Product.Infrastructure.Tests.Utils.HelperTypes;

namespace Product.Infrastructure.Tests.Utils;

public class TestMapperProfile : Profile
{
   public TestMapperProfile()
   {
      CreateMap<HelperProductDTO, Domain.Entities.Product>().ForMember(d => d.MainImage, opt => opt.MapFrom(s => new Blob("", s.MainImagePath, "")))
          .ReverseMap();
   }
}

