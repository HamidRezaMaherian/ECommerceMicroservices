﻿using AutoMapper;

namespace Product.Infrastructure.Tests.Utils
{
	public static class UtilsExtension
	{
		public static Mapper CreateMapper<T1, T2>()
		{
			var automapperConfig = new MapperConfiguration(i => i.CreateMap<T1, T2>().ReverseMap());
			return new Mapper(automapperConfig);
		}
	}
}