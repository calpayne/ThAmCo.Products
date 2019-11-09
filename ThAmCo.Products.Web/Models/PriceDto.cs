using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Products.Web.Models
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
