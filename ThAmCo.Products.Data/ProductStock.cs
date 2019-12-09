using System;
using System.Collections.Generic;
using System.Text;

namespace ThAmCo.Products.Data
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockLevel { get; set; }
    }
}
