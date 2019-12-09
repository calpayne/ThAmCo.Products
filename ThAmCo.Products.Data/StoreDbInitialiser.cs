using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Products.Data
{
    public static class StoreDbInitialiser
    {
        public static async Task SeedTestData(StoreDb context, IServiceProvider services)
        {
            if (context.ProductStock.Any())
            {
                //db seems to be seeded
                return;
            }

            int numOfProducts = 17;
            Random random = new Random();
            var productStock = new List<ProductStock>();
            for (int i = 1; i <= numOfProducts-1; i++)
            {
                productStock.Add(new ProductStock { ProductId = i, StockLevel = random.Next(0, 20) });
            }
            productStock.Add(new ProductStock { ProductId = numOfProducts, StockLevel = 0 });
            productStock.ForEach(p => context.ProductStock.Add(p));
            await context.SaveChangesAsync();

            var priceHistory = new List<PriceHistory>();
            for(int i = 1; i <= numOfProducts; i++)
            {
                int times = random.Next(0, 20);
                for (int j = 0; j < times; j++)
                {
                    priceHistory.Add(new PriceHistory { ProductId = i, Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), CreatedDate = DateTime.Now });
                }
            }
            priceHistory.ForEach(p => context.PriceHistory.Add(p));
            await context.SaveChangesAsync();
        }
    }
}
