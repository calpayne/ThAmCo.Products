using ThAmCo.Products.Data;

namespace ThAmCo.Products.Models
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static MaterialDto Transform(Material m)
        {
            return new MaterialDto
            {
                Id = m.Id,
                Name = m.Name
            };
        }

        public static Material ToMaterial(MaterialDto m)
        {
            return new Material
            {
                Id = m.Id,
                Name = m.Name,
                Active = true
            };
        }
    }
}
