using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Services.Shared.APIUtils
{
	public class HttpRequestHelper : IDisposable
	{
		private readonly HttpClient _httpClient;

		public List<KeyValuePair<string, IEnumerable<string>>> DefaultRequestHeaders { get; set; }

		public HttpRequestHelper(HttpClient httpClient)
		{
			_httpClient = httpClient;
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
			string jsonString;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.GetAsync(partialUrl).Result;
			if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gZip"))
			{
				using var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result;
				using Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression);
				using var streamReader = new StreamReader(streamDecompressed);
				jsonString = streamReader.ReadToEnd();
			}
			else
			{
				jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
			}

			return JsonSerializer.Deserialize<TResponse>(jsonString);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
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
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public TResponse Post<TResponse>(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			string jsonString;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var stringContent = new StringContent("");
			var httpResponseMessage = _httpClient.PostAsync(partialUrl, stringContent).Result;
			if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gzip"))
			{
				using var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result;
				using Stream streamDecompressed = new GZipStream(stream, CompressionMode.Decompress);
				using var streamReader = new StreamReader(streamDecompressed);
				jsonString = streamReader.ReadToEnd();
			}
			else
			{
				jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
			}

			return JsonSerializer.Deserialize<TResponse>(jsonString);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public TResponse Post<TResponse>(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			string jsonRequest;
			string jsonString;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			jsonRequest = JsonSerializer.Serialize(request);
			var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			var httpResponseMessage = _httpClient.PostAsync(partialUrl, stringContent).Result;
			if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gzip"))
			{
				using var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result;
				using Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression);
				using var streamReader = new StreamReader(streamDecompressed);
				jsonString = streamReader.ReadToEnd();
			}
			else
			{
				jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
			}

			return JsonSerializer.Deserialize<TResponse>(jsonString);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public HttpResponseMessage Post(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			string jsonRequest;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			jsonRequest = JsonSerializer.Serialize(request);
			var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			var httpResponseMessage = _httpClient.PostAsync(partialUrl, stringContent).Result;
			return httpResponseMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public TResponse Put<TResponse>(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			string jsonRequest;
			string jsonString;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			jsonRequest = JsonSerializer.Serialize(request);
			var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			var httpResponseMessage = _httpClient.PutAsync(partialUrl, stringContent).Result;
			if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gzip"))
			{
				using var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result;
				using Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression);
				using var streamReader = new StreamReader(streamDecompressed);
				jsonString = streamReader.ReadToEnd();
			}
			else
			{
				jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
			}

			return JsonSerializer.Deserialize<TResponse>(jsonString);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public HttpResponseMessage Put(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			string jsonRequest;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			jsonRequest = JsonSerializer.Serialize(request);
			var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
			var httpResponseMessage = _httpClient.PutAsync(partialUrl, stringContent).Result;
			return httpResponseMessage;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public TResponse Delete<TResponse>(string partialUrl,List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false)
		{
			string jsonString;
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			if (gZip)
			{
				_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			}

			SetHeaders(headers, _httpClient);

			var httpResponseMessage = _httpClient.DeleteAsync(partialUrl).Result;
			if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gzip"))
			{
				using var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result;
				using Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression);
				using var streamReader = new StreamReader(streamDecompressed);
				jsonString = streamReader.ReadToEnd();
			}
			else
			{
				jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
			}

			return JsonSerializer.Deserialize<TResponse>(jsonString);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
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

		#region Async

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public async Task<TResponse> PostAsync<TResponse>(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false, CancellationToken token = default)
		{

			try
			{
				string jsonRequest;
				string jsonString;
				_httpClient.DefaultRequestHeaders.Accept.Clear();
				if (gZip)
				{
					_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(
						 new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
				}

				SetHeaders(headers, _httpClient);

				jsonRequest = JsonSerializer.Serialize(request);
				var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
				var httpResponseMessage = await _httpClient.PostAsync(partialUrl, stringContent, token);
				if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gzip"))
				{
					using (var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result)
					using (Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression))
					using (var streamReader = new StreamReader(streamDecompressed))
					{
						jsonString = streamReader.ReadToEnd();
					}
				}
				else
				{
					jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
				}

				return JsonSerializer.Deserialize<TResponse>(jsonString);
			}
			catch (Exception)
			{
				// log it or send sms
			}

			return default(TResponse);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="request">شئ حاوی پارامتر های نوع مدل ارسالی</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> PostAsync(string partialUrl, object request, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false, CancellationToken token = default)
		{
			try
			{
				string jsonRequest;
				_httpClient.DefaultRequestHeaders.Accept.Clear();
				if (gZip)
				{
					_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(
						 new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
				}

				SetHeaders(headers, _httpClient);

				jsonRequest = JsonSerializer.Serialize(request);
				var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
				var httpResponseMessage = await _httpClient.PostAsync(partialUrl, stringContent, token);
				return httpResponseMessage;
			}
			catch (Exception)
			{
				// log it or send sms
			}

			return default(HttpResponseMessage);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public async Task<TResponse> PostAsync<TResponse>(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false, CancellationToken token = default)
		{
			try
			{
				string jsonString;
				_httpClient.DefaultRequestHeaders.Accept.Clear();
				if (gZip)
				{
					_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(
						 new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
				}

				SetHeaders(headers, _httpClient);

				var stringContent = new StringContent("");
				var httpResponseMessage = await _httpClient.PostAsync(partialUrl, stringContent, token);
				if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gzip"))
				{
					using (var stream = httpResponseMessage.Content.ReadAsStreamAsync().Result)
					using (Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression))
					using (var streamReader = new StreamReader(streamDecompressed))
					{
						jsonString = streamReader.ReadToEnd();
					}
				}
				else
				{
					jsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;
				}

				return JsonSerializer.Deserialize<TResponse>(jsonString);
			}
			catch (Exception)
			{
				// log it or send sms
			}

			return default(TResponse);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public async Task<TResponse> GetAsync<TResponse>(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false, CancellationToken token = default)
		{
			try
			{
				string jsonString;
				_httpClient.DefaultRequestHeaders.Accept.Clear();
				if (gZip)
				{
					_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
				}

				SetHeaders(headers, _httpClient);

				var httpResponseMessage = await _httpClient.GetAsync(partialUrl, token);
				if (httpResponseMessage.Content.Headers.ContentEncoding.Any(x => x == "gZip"))
				{
					using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
					using (Stream streamDecompressed = new GZipStream(stream, CompressionLevel.NoCompression))
					using (var streamReader = new StreamReader(streamDecompressed))
					{
						jsonString = streamReader.ReadToEnd();
					}
				}
				else
				{
					jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
				}

				return JsonSerializer.Deserialize<TResponse>(jsonString);
			}
			catch (Exception)
			{
				// log it or send sms
			}

			return default(TResponse);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResponse">نوع مدل بازگشتی</typeparam>
		/// <param name="partialUrl">ادامه آدرس درخواستی بعد از نام هاست</param>
		/// <param name="headers"></param>
		/// <param name="gZip">فلگ فعال سازی دیتا برای فشرده سازی</param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetAsync(string partialUrl, List<KeyValuePair<string, IEnumerable<string>>> headers = null, bool gZip = false, CancellationToken token = default)
		{
			try
			{
				_httpClient.DefaultRequestHeaders.Accept.Clear();
				if (gZip)
				{
					_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
				}

				SetHeaders(headers, _httpClient);

				var httpResponseMessage = await _httpClient.GetAsync(partialUrl, token);
				return httpResponseMessage;
			}
			catch (Exception)
			{
				// log it or send sms
			}

			return default(HttpResponseMessage);
		}

		#endregion Async

		#region Common

		public void SetHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, HttpClient _httpClient)
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
