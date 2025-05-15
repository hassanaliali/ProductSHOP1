using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Entities;
using Product.Infrastructure.Data.DTOs;

namespace Product.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<ProductEntity>
    {
        Task<bool> AddAsync(AddProductDTO productDto);
    }
}
