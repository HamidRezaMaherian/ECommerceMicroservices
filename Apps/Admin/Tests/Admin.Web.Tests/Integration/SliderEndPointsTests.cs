using Admin.Web.Tests.Utils;
using NUnit.Framework;
using Services.Shared.APIUtils;

namespace Admin.Web.Tests.Integration
{
	[TestFixture]
	public class SliderEndPointsTests
	{
		private HttpRequestHelper _httpClient;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			var httpClient = new TestingWebAppFactory<Program>(s =>
			{
				//var dbContextConfiguration = s.SingleOrDefault(opt => opt.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
				//if (dbContextConfiguration != null)
				//	s.Remove(dbContextConfiguration);
				//s.AddDbContextPool<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(dbName));
			}).CreateClient();
			_httpClient = new HttpRequestHelper(httpClient);
		}
		[TearDown]
		public void TearDown()
		{
			//var percentDiscounts = _unitOfWork.PercentDiscountRepo.Get();
			//foreach (var item in percentDiscounts)
			//{
			//	_unitOfWork.PercentDiscountRepo.Delete(item);
			//}
			//var priceDiscounts = _unitOfWork.PriceDiscountRepo.Get();
			//foreach (var item in priceDiscounts)
			//{
			//	_unitOfWork.PriceDiscountRepo.Delete(item);
			//}
			//_unitOfWork.Save();
		}
		[Test]
		public void HealthCheck_IsOk()
		{
			var res = _httpClient.Get("/health");
			res.EnsureSuccessStatusCode();
		}
	}
}
