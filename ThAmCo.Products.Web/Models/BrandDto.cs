using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Products.Data;

namespace ThAmCo.Products.Web.Models
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static BrandDto Transform(Brand b)
        {
            return new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description
            };
        }

        public static Brand ToBrand(BrandDto b)
        {
            return new Brand
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Active = true
            };
        }
    }
}
