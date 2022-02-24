using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleEcommerce.Domain.Models
{
    public class CategoryProperty: EntityFlagBase
    {
        [Required]
        public string PropertyId { get; set; }
        [Required]
        public string CategoryId { get; set; }
        #region NavigationProps
        public virtual ProductCategory Category { get; set; }
        public virtual Property Property { get; set; }
        #endregion
    }
}
