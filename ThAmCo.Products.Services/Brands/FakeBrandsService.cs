using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Brands
{
    public class FakeBrandsService : IBrandsService
    {
        public Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BrandDto> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
