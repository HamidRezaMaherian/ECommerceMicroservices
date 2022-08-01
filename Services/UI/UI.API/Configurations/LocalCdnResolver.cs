using UI.Application.Tools;

namespace UI.API.Configurations
{
	public class LocalCdnResolver : ICdnResolver
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LocalCdnResolver(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public string GetAddress()
		{
			var httpContext = _httpContextAccessor.HttpContext ?? throw new NullReferenceException("httpCotext is null");
			return httpContext.Request.IsHttps ? $"https://{httpContext.Request.Host.Value}" : $"http://{httpContext.Request.Host.Value}";
		}
	}
}
