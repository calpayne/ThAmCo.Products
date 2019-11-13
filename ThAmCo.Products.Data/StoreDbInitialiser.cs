using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThAmCo.Products.Data
{
    public static class StoreDbInitialiser
    {
        public static async Task SeedTestData(StoreDb context, IServiceProvider services)
        {
            if (context.Products.Any())
            {
                //db seems to be seeded
                return;
            }

            Random random = new Random();
            var products = new List<Product>
            {
                new Product { BrandId = 1, CategoryId = 1, Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Purchase you favourite chocolate and use the provided heating element to melt it into the perfect cover for your mobile device.", Name = "Chocolate Cover", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Lamely adapted used and dirty teatowel.  Guaranteed fewer than two holes.", Name = "Cloth Cover", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Especially toughen and harden sponge entirely encases your device to prevent any interaction.", Name = "Harden Sponge Case", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Place your device within the water-tight container, fill with water and enjoy the cushioned protection from bumps and bangs.", Name = "Water Bath Case", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Keep you smartphone handsfree with this large assembly that attaches to your rear window wiper (Hatchbacks only).", Name = "Smartphone Car Holder", Price = Math.Round(100 + (random.NextDouble() * (1000 - 100)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Keep your device on your arm with this general purpose sticky tape.", Name = "Sticky Tape Sport Armband", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Stengthen HB pencils guaranteed to leave a mark.", Name = "Real Pencil Stylus", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Coat your mobile device screen in a scratch resistant, opaque film.", Name = "Spray Paint Screen Protector", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "For his or her sensory pleasure. Fits few known smartphones.", Name = "Rippled Screen Protector", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "For an odour than lingers on your device.", Name = "Fish Scented Screen Protector", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { BrandId = 1, CategoryId = 1, Description = "Guaranteed not to conduct electical charge from your fingers.", Name = "Non-conductive Screen Protector", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = 0, Active = true }
            };
            products.ForEach(p => context.Products.Add(p));
            await context.SaveChangesAsync();

            var pricehistory = new List<PriceHistory>();
            for(int i = 0; i < products.Count(); i++)
            {
                int times = random.Next(0, 20);
                for (int j = 0; j < times; j++)
                {
                    pricehistory.Add(new PriceHistory { Product = products[i], Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), CreatedDate = DateTime.Now });
                }
            }
            pricehistory.ForEach(p => context.PriceHistory.Add(p));
            await context.SaveChangesAsync();
        }
    }
}
