using System.Text.Json;

namespace Services.Shared.AppUtils
{
	public static class JsonHelper
	{
		public static T Parse<T>(string content)
		{
			return JsonSerializer.Deserialize<T>(content);
		}
		public static string Stringify(object content)
		{
			return JsonSerializer.Serialize(content);
		}
	}
}
