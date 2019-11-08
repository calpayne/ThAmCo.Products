using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Products.Data;

namespace ThAmCo.Products.Web.Models
{
    public class TypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static TypeDto Transform(PType t)
        {
            return new TypeDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            };
        }

        public static PType ToType(TypeDto b)
        {
            return new PType
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Active = true
            };
        }
    }
}
