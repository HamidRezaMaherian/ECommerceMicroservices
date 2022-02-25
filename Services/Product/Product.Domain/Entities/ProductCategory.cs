using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class ProductCategory : EntityBase<string>
    {
        public virtual string Name { get; set; }

        #region Relations
        public string? ParentId { get; set; }
        public ProductCategory Parent { get; set; }
        #endregion
    }
}
