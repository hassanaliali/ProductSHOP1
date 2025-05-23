﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    public class ProductEntity:BasicEntity
    {
        //public int Id { set; get; }
        public String Name { set; get; }
        public String Description { set; get; }
        public double Price { set; get; }
        public String Photo { set; get; }
        public int CategoryId { set; get; }
        public virtual Category category { set; get; }
    }
}
