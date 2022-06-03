using Admin.Application.Models.UI;

namespace Admin.Application.Services.UI
{
	public interface IFaqService: IQueryBaseService<Faq>, ICommandBaseService<Faq, Faq>
	{
	}
}
