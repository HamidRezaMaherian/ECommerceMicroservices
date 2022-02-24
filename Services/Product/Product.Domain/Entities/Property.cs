using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleEcommerce.Domain.Models
{
    public class Property : EntityBase<string>
    {
        [MaxLength(300)]
        [Required]
        public string Name { get; set; }
        public PropertyType Type { get; set; }
    }
    public enum PropertyType
    {
        Number,
        String
    }
}
