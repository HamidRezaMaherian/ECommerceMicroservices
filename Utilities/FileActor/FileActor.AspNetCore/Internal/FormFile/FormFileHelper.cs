using FileActor.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileActor.AspNetCore.Internal
{
	public class FormFileStream : FileTypeHelper<IFormFile>
	{
		public override void Upload(IFormFile file, string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var fullPath = Path.Combine(path, filename);
			using (var fs = new FileStream(fullPath, FileMode.Create))
			{
				file.CopyTo(fs);
			}
		}

		public override async Task UploadAsync(IFormFile file, string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var fullPath = Path.Combine(path, filename);
			using (var fs = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(fs);
			}
		}
		public override string GetExtension(IFormFile file)
		{
			return Path.GetExtension(file.FileName);
		}

	}
}
