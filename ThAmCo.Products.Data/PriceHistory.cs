using System;

namespace ThAmCo.Products.Data
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public Product Product { get; set; }
    }
}
