using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data.DTOs
{
    public class CategoryDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }

    public class ListCategoryDTO:CategoryDTO
    {
        [Required]
        public int Id { get; set; }
        
    }
}
