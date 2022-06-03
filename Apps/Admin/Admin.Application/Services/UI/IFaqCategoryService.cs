using Admin.Application.Models.UI;

namespace Admin.Application.Services.UI
{
	public interface IFaqCategoryService:IQueryBaseService<FaqCategory>,ICommandBaseService<FaqCategory,FaqCategory>
	{
	}
}
