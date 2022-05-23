using FileActor.Abstract;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FileActor.AspNetCore.Internal
{
	public class FormFileExtension : FileExtension<IFormFile>
	{
		public override string GetExtension(IFormFile file)
		{
			return Path.GetExtension(file.FileName);
		}
	}
}
