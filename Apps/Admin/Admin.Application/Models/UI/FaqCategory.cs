namespace Admin.Application.Models.UI
{
	public class FaqCategory
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<Faq> FAQs { get; set; }
	}
}
