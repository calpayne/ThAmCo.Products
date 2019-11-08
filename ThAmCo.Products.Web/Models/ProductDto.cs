using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Products.Data;

namespace ThAmCo.Products.Web.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get { return "£"; } }
        public double Price { get; set; }
        public int StockLevel { get; set; }

        public TypeDto Type { get; set; }
        public MaterialDto Material { get; set; }
        public BrandDto Brand { get; set; }

        public static ProductDto Transform(Product p)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockLevel = p.StockLevel,
                Type = TypeDto.Transform(p.Type),
                Material = MaterialDto.Transform(p.Material),
                Brand = BrandDto.Transform(p.Brand)
            };
        }

        public static Product ToProduct(ProductDto p)
        {
            return new Product
            {
                Id = p.Id,
                TypeId = p.Type.Id,
                MaterialId = p.Material.Id,
                BrandId = p.Brand.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockLevel = p.StockLevel,
                Active = true,
                Type = TypeDto.ToType(p.Type),
                Material = MaterialDto.ToMaterial(p.Material),
                Brand = BrandDto.ToBrand(p.Brand)
            };
        }
    }
}
