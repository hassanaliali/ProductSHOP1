using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Entities;

namespace Product.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<ProductEntity>
    {
    }
}
