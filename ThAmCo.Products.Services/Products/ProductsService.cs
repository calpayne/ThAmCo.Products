using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Data;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly StoreDb _context;
        private readonly HttpClient _client;

        public ProductsService(StoreDb context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        private async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            IEnumerable<ProductDto> products;
            HttpResponseMessage response = await _client.GetAsync("Product");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            try
            {
                response.EnsureSuccessStatusCode();
                products = await response.Content.ReadAsAsync<IEnumerable<ProductDto>>();
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (UnsupportedMediaTypeException)
            {
                return null;
            }

            foreach (ProductDto p in products)
            {
                try
                {
                    p.Price = _context.PriceHistory.OrderByDescending(d => d.CreatedDate).FirstOrDefault(pr => pr.ProductId == p.Id).Price;
                    p.StockLevel = _context.ProductStock.FirstOrDefault(ps => ps.ProductId == p.Id).StockLevel;
                }
                catch(Exception)
                {
                    p.Price = 0;
                    p.StockLevel = 0;
                }
            }

            return products;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(int[] brands, int[] categories, string term, double? minPrice, double? maxPrice)
        {
            IEnumerable<ProductDto> products = await GetAllProducts();

            return products.Where(p => term == null || (p.Name.Contains(term) || p.Description.Contains(term)))
                           .Where(p => brands.Count() == 0 || brands.Contains(p.BrandId))
                           .Where(p => categories.Count() == 0 || categories.Contains(p.CategoryId))
                           .Where(p => minPrice == null || p.Price >= minPrice)
                           .Where(p => maxPrice == null || p.Price <= maxPrice);
        }

        public async Task<IEnumerable<ProductDto>> GetAllByStockAsync()
        {
            IEnumerable<ProductDto> products = await GetAllProducts();

            return products.OrderByDescending(p => p.StockLevel);
        }

        public async Task<ProductDto> GetByIDAsync(int id)
        {
            ProductDto product;
            HttpResponseMessage response = await _client.GetAsync("Product/" + id);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            try
            {
                response.EnsureSuccessStatusCode();
                product = await response.Content.ReadAsAsync<ProductDto>();
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (UnsupportedMediaTypeException)
            {
                return null;
            }

            try
            {
                product.Price = _context.PriceHistory.OrderByDescending(d => d.CreatedDate).FirstOrDefault(pr => pr.ProductId == product.Id).Price;
                product.StockLevel = _context.ProductStock.FirstOrDefault(ps => ps.ProductId == product.Id).StockLevel;
            }
            catch (Exception)
            {
                product.Price = 0;
                product.StockLevel = 0;
            }

            return product;
        }

        public async Task<IEnumerable<PriceHistoryDto>> GetPriceHistoryAsync(int id)
        {
            return await _context.PriceHistory.Where(p => p.ProductId == id)
                                              .Select(p => PriceHistoryDto.Transform(p))
                                              .ToListAsync();
        }

        public async Task<bool> UpdateProductStockAsync(int id, int newStock)
        {
            ProductStock stock = _context.ProductStock.FirstOrDefault(s => s.ProductId == id);

            if (stock == null)
            {
                return false;
            }

            stock.StockLevel = newStock;
            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProductStock.Any(e => e.ProductId == id))
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
