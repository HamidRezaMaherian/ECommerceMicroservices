using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Services.Shared.Contracts;
using Services.Shared.Mapper;

namespace Services.Shared.Tests
{
	public static class UtilsExtension
	{
		public static ICustomMapper CreateMapper<T1, T2>()
		{
			var automapperConfig = new MapperConfiguration(i => i.CreateMap<T1, T2>().ReverseMap());

			return new CustomMapper(new AutoMapper.Mapper(automapperConfig));
		}
		public static ICustomMapper CreateMapper(params Profile[] profiles)
		{
			var automapperConfig = new MapperConfiguration(i => i.AddProfiles(profiles));
			return new CustomMapper(new AutoMapper.Mapper(automapperConfig));
		}
	}
	public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
	{
		private readonly Action<IServiceCollection> _mockConfigureServices;

		public TestingWebAppFactory(Action<IServiceCollection> mockConfigureServices)
		{
			_mockConfigureServices = mockConfigureServices;
		}
		public TestingWebAppFactory()
		{

		}
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			if (_mockConfigureServices != null)
				builder.ConfigureServices(_mockConfigureServices);
			else
				base.ConfigureWebHost(builder);
		}
	}
}
