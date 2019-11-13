using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Brands
{
    public interface IBrandsService
    {
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> GetByIDAsync(int id);
    }
}
