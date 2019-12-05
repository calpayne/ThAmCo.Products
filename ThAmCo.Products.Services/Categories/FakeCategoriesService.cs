using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Categories
{
    public class FakeCategoriesService : ICategoriesService
    {
        private readonly IEnumerable<CategoryDto> _categories;

        public FakeCategoriesService()
        {
            _categories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Covers", Description = "Davison Stores pride ourselves on our poor range of covers for your mobile device at premium prices.  If you're lukcy your phone or tablet will be protected from any dents, scratches and scuffs." },
                new CategoryDto { Id = 2, Name = "Case", Description = "Browse our wide range of cases for phones and tablets that will help you to keep your mobile device protected from the elements." },
                new CategoryDto { Id = 3, Name = "Accessories", Description = "We stock a small range of phone and tablet accessories, including car holders, sports armbands, stylus pens and very little else." },
                new CategoryDto { Id = 4, Name = "Screen Protectors", Description = "Exclusive Davison Stores screen protectors for your phone or tablet." }
            };
        }

        public Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return Task.FromResult(_categories);
        }

        public Task<CategoryDto> GetByIDAsync(int id)
        {
            return Task.FromResult(_categories.FirstOrDefault(b => b.Id == id));
        }
    }
}
