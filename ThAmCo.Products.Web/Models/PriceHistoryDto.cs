using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Products.Data;

namespace ThAmCo.Products.Web.Models
{
    public class PriceHistoryDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public static PriceHistoryDto Transform(PriceHistory p)
        {
            return new PriceHistoryDto
            {
                Id = p.Id,
                Price = p.Price,
                CreatedDate = p.CreatedDate
            };
        }
    }
}
