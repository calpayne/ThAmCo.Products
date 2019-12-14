using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Products.Models
{
    public class OrderDto
    {
        [Required]
        public ProductDto Product { get; set; }
        [Required]
        public CustomerDto Customer { get; set; }
    }
}
