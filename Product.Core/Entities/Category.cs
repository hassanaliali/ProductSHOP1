using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    public class Category
    {
        public int Id { set; get; }
        public String Name { set; get; } = "";
        public String Description { set; get; } = "";

        public virtual ICollection<ProductEntity> products { set; get; } =new HashSet<ProductEntity>();
    }
}
