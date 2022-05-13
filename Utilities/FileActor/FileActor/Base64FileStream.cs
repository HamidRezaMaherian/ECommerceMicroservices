using FileActor.Abstract;
using System;
using System.IO;

namespace FileActor
{
	public class Base64FileStream : IFileStream<string>
	{
		public void Upload(string file, string path)
		{
         var splitedBase64 = file.Split(",");
         var extension = "." + splitedBase64[0].Split('/', ';')[1];
         var imageBase64 = splitedBase64[1];

         var filename = Guid.NewGuid().ToString() + extension;

         string filePath = Path.Combine();
         if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

         var fullPath = Path.Combine(filePath, filename);

         File.Create(fullPath).Close();
         byte[] bytes = Convert.FromBase64String(imageBase64);

         File.WriteAllBytes(fullPath, bytes);
      }
   }
}
