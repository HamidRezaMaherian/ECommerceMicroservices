using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEcommerce.Domain.Models
{
    public class ProductCategory : EntityBase<string>
    {
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        //[Required]
        //public string ImagePath { get; set; }
        #region Relations

        public string? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual ProductCategory Parent { get; set; }
        #endregion
    }
}
