using SampleEcommerce.Application.DTOs;
using SampleEcommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public interface IProductService : IEntityBaseService<Product>
    {
        new IEnumerable<ProductDTO> GetAll();
        new ProductDTO GetById(object id);
        new IEnumerable<ProductDTO> GetAllActive();
    }
}
