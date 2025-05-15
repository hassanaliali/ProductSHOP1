using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Product.Infrastructure.Data.DTOs
{
    public class ProductBasic
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(1,9999,ErrorMessage="Price limeted by {0} and {1}")]
        [RegularExpression(@"[0-9]*\.?[0-9]+",ErrorMessage ="{0} must be number")]
        public double Price { get; set; }
    }
    public class ProductDTO:ProductBasic
    {
        
        public string CategoryName { get; set; }
    }
    public class ListProductDTO: ProductBasic
    {
        [Required]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        
    }
    public class AddProductDTO: ProductBasic
    {
        [Required]

        public int CategoryId { get; set; }
        public IFormFile image { get; set; }

    }
    public class UpdateProductDTO: ProductBasic
    {
        [Required]
        public int Id { get; set; }
        public int CategoryId { get; set; }

    }
}
