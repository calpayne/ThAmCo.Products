using ThAmCo.Products.Data;

namespace ThAmCo.Products.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get { return "£"; } }
        public double Price { get; set; }
        public int StockLevel { get; set; }

        public static ProductDto Transform(Product p)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockLevel = p.StockLevel,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId
            };
        }

        public static Product ToProduct(ProductDto p)
        {
            return new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockLevel = p.StockLevel,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                Active = true
            };
        }
    }
}
