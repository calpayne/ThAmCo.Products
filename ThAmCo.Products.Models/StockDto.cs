using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Products.Models
{
    public class StockDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int AdditionalStock { get; set; }
    }
}
