using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEcommerce.Domain.Models
{
    public abstract class ProductDiscount : IBaseDelete
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        #region Relations
        [Required]
        [Key]
        public string ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
        public bool IsDelete { get; set; }
    }
    public class ProductPriceDiscount : ProductDiscount
    {
        [Required]
        public decimal Price { get; set; }
    }
    public class ProductPercentDiscount : ProductDiscount
    {
        [Range(1, 100)]
        [Required]
        public int Percent { get; set; }
    }
}
