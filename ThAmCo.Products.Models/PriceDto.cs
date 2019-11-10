using System;

namespace ThAmCo.Products.Models
{
    public class PriceDto
    {
        public int Id { get; set; }
        public double ResalePrice
        {
            get { return Math.Round(ResalePrice, 2); }
            set { ResalePrice = value; }
        }
    }
}
