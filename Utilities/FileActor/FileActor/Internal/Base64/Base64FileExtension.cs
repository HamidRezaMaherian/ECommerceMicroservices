using FileActor.Abstract;
using System.Collections.Generic;

namespace FileActor.Internal
{
	public class Base64FileExtension : FileExtension<string>
	{
		public override string GetExtension(string file)
		{
         return "." + GetTypeExtension(file[..5]);
		}
		private string GetTypeExtension(string data)
		{
         switch (data.ToUpper())
         {
            case "IVBOR":
               return "png";
            case "/9J/4":
               return "jpg";
            case "AAAAF":
               return "mp4";
            case "JVBER":
               return "pdf";
            case "AAABA":
               return "ico";
            case "UMFYI":
               return "rar";
            case "E1XYD":
               return "rtf";
            case "U1PKC":
               return "txt";
            case "MQOWM":
            case "77U/M":
               return "srt";
            default:
               return string.Empty;
         }
      }
	}
}
