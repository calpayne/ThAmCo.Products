using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Data;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly StoreDb _context;

        public ProductsService(StoreDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(int[] brands, int[] categories, string term, double? minPrice, double? maxPrice)
        {
            return await _context.Products.Where(p => term == null || (p.Name.Contains(term) || p.Description.Contains(term)))
                                          .Where(p => brands.Count() == 0 || brands.Contains(p.BrandId))
                                          .Where(p => categories.Count() == 0 || categories.Contains(p.CategoryId))
                                          .Where(p => minPrice == null || p.Price >= minPrice)
                                          .Where(p => maxPrice == null || p.Price <= maxPrice)
                                          .Select(p => ProductDto.Transform(p))
                                          .ToListAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllByStockAsync()
        {
            return await _context.Products.OrderByDescending(p => p.StockLevel)
                                         .Select(p => ProductDto.Transform(p))
                                         .ToListAsync();
        }

        public async Task<ProductDto> GetByIDAsync(int id)
        {
            return await _context.Products.Select(p => ProductDto.Transform(p))
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PriceHistoryDto>> GetPriceHistoryAsync(int id)
        {
            return await _context.PriceHistory.Where(p => p.ProductId == id)
                                              .Select(p => PriceHistoryDto.Transform(p))
                                              .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto product)
        {
            _context.Entry(ProductDto.ToProduct(product)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
