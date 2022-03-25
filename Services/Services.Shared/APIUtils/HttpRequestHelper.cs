using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;
using Services.Shared.Behaviours;
using System.Net.Http.Json;
namespace Services.Shared.APIUtils
{
	public class HttpRequestHelper : IDisposable
	{
		private readonly HttpClient _httpClient;

		public List<KeyValuePair<string, IEnumerable<string>>> DefaultRequestHeaders { get; set; }

		public HttpRequestHelper(HttpClient httpClient)
		{
			_httpClient = httpClient;
			LoggingServices.DefaultBackend = new ConsoleLoggingBackend();
		}

		#region Non Async

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public TResponse Get<TResponse>(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.GetFromJsonAsync<TResponse>(partialUrl).Result;

			return httpResponseMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		[LoggingAspect]
		public HttpResponseMessage Get(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.GetAsync(partialUrl).Result;
			return httpResponseMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		[LoggingAspect]
		public HttpResponseMessage Post(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.PostAsJsonAsync(partialUrl, request).Result;
			return httpResponseMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		[LoggingAspect]
		public HttpResponseMessage Put(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.PutAsJsonAsync(partialUrl, request).Result;
			return httpResponseMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		[LoggingAspect]
		public HttpResponseMessage Delete(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.DeleteAsync(partialUrl).Result;
			return httpResponseMessage;
		}

		#endregion
		#region Common

		private void SetHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, HttpClient _httpClient)
		{
			//Set global headers.
			if (DefaultRequestHeaders != null)
			{
				foreach (var defaultRequestHeader in DefaultRequestHeaders)
				{
					_httpClient.DefaultRequestHeaders.Add(defaultRequestHeader.Key, defaultRequestHeader.Value);
				}
			}

			//Set local headers.
			if (headers != null)
			{
				foreach (var header in headers)
				{
					_httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
				}
			}
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}
		#endregion

	}
}
