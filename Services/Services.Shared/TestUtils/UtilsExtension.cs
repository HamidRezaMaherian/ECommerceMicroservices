using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Services.Shared.Tests
{
	public static class UtilsExtension
	{
		public static Mapper CreateMapper<T1, T2>()
		{
			var automapperConfig = new MapperConfiguration(i => i.CreateMap<T1, T2>().ReverseMap());
			return new Mapper(automapperConfig);
		}
   }
   public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
   {
      protected override void ConfigureWebHost(IWebHostBuilder builder)
      {
      }
   }
}
