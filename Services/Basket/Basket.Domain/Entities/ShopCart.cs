using Basket.Domain.Common;

namespace Basket.Domain.Entities
{
	public class ShopCart : EntityPrimaryBase<string>
	{
		public override string Id { get => UserName; set => base.Id = value; }
		public string UserName { get; set; }
		public IEnumerable<ShopCartItem> Items { get; set; }
		public uint TotalPrice
		{
			get
			{
				return (uint)(Items?.Sum(x => x.Price) ?? 0);
			}
		}
	}
}
