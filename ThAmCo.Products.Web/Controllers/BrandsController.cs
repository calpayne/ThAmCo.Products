using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Brands;

namespace ThAmCo.Products.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsService _brands;

        public BrandsController(IBrandsService brands)
        {
            _brands = brands;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _brands.GetAllAsync();

            if (brands == null)
            {
                return NoContent();
            }

            return Ok(brands.ToList());
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _brands.GetByIDAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }
    }
}
