using System;
using System.Collections.Generic;
using System.Text;

namespace ThAmCo.Products.Data
{
    public class Product
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int MaterialId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockLevel { get; set; }
        public bool Active { get; set; }

        public PType Type { get; set; }
        public Material Material { get; set; }
        public Brand Brand { get; set; }
    }
}
