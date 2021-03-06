using Inventory.Domain.Common;

namespace Inventory.Application.DTOs
{
	public class StoreDTO:EntityBase<string>
	{
		public string Name { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }
		public string LogoPath { get; set; }
	}
}
