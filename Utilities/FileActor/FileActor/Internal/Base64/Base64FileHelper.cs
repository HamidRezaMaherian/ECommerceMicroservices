using FileActor.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileActor.Internal
{
	public class Base64FileHelper : FileTypeHelper<string>
   {
		public override void Upload(string file, string path)
		{
         File.Create(path).Close();
         byte[] bytes = Convert.FromBase64String(file);
         File.WriteAllBytes(path, bytes);
      }

		public override async Task UploadAsync(string file, string path)
		{
         var splitedBase64 = file.Split(",");
         var imageBase64 = splitedBase64[1];

         File.Create(path).Close();
         byte[] bytes = Convert.FromBase64String(imageBase64);

         await File.WriteAllBytesAsync(path, bytes);
      }
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
