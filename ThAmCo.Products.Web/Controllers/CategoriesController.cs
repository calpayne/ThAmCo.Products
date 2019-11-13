using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Categories;

namespace ThAmCo.Products.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categories;

        public CategoriesController(ICategoriesService categories)
        {
            _categories = categories;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categories.GetAllAsync();

            if (categories == null)
            {
                return Array.Empty<CategoryDto>();
            }

            return categories.ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categories.GetByIDAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
    }
}
