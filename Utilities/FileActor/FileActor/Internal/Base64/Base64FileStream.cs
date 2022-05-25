using FileActor.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileActor.Internal
{
	public class Base64FileStream : FileStream<string>
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
   }
}
