﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleEcommerce.Domain.Models
{
    public class ProductStock : IBaseDelete
    {
        [Key]
        [Required]
        public string ProductId { get; set; }
        public int Count { get; set; }
        #region Relations
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        #endregion
        public bool IsDelete { get; set; }
    }
}
