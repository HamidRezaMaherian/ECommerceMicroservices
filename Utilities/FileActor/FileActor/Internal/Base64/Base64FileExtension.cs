using FileActor.Abstract;

namespace FileActor.Internal
{
	public class Base64FileExtension : FileExtension<string>
	{
		public override string GetExtension(string file)
		{
			var splitedBase64 = file.Split(",");
			return "." + splitedBase64[0].Split('/', ';')[1];
		}
	}
}
