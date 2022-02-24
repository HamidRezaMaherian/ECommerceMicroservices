using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleEcommerce.Domain.Models
{
    public class ProductImage : EntityBase<string>
    {
        [Required]
        public string ImagePath { get; set; }
        #region Relations
        [Required]
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
    }
}
