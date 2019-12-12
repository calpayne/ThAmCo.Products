using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(int[] brands, int[] categories, string term, double? minPrice, double? maxPrice);
        Task<IEnumerable<ProductDto>> GetAllByStockAsync();
        Task<ProductDto> GetByIDAsync(int id);
        Task<bool> UpdateProductStockAsync(int id, int newStock);
        Task<bool> UpdateProductPriceAsync(PriceDto price);
        Task<bool> UpdateProductStockAsync(StockDto stock);
        Task<IEnumerable<PriceHistoryDto>> GetPriceHistoryAsync(int id);
    }
}
