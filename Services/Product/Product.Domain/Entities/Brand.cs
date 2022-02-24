using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleEcommerce.Domain.Models
{
    public class Brand : EntityBase<string>
    {
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        #region NavigationProps
        public virtual Product Products { get; set; }
        #endregion
    }
}
