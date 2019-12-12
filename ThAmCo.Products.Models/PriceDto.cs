using System;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Products.Models
{
    public class PriceDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required, Range(0.01, Double.MaxValue)]
        public double ResalePrice { get; set; }
    }
}
