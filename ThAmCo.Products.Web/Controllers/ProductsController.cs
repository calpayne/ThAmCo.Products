using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Products.Data;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Orders;

namespace ThAmCo.Products.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDb _context;
        private readonly IOrdersService _ordersService;

        public ProductsController(StoreDb context, IOrdersService ordersService)
        {
            _context = context;
            _ordersService = ordersService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Brand)
                                          .Include(p => p.Material)
                                          .Include(p => p.Type)
                                          .Select(p => ProductDto.Transform(p))
                                          .ToListAsync();
        }

        // GET: api/Products/Search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int? brand, int? material, int? type, string term, double? minPrice, double? maxPrice)
        {
            return await _context.Products.Include(p => p.Brand)
                                          .Include(p => p.Material)
                                          .Include(p => p.Type)
                                          .Where(p => term == null || (p.Name.Contains(term) || p.Description.Contains(term)))
                                          .Where(p => brand == null || p.BrandId == brand)
                                          .Where(p => material == null || p.MaterialId == material)
                                          .Where(p => type == null || p.TypeId == type)
                                          .Where(p => minPrice == null || p.Price >= minPrice)
                                          .Where(p => maxPrice == null || p.Price <= maxPrice)
                                          .Select(p => ProductDto.Transform(p))
                                          .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Brand)
                                                 .Include(p => p.Material)
                                                 .Include(p => p.Type)
                                                 .Select(p => new ProductDto
                                                 {
                                                     Id = p.Id,
                                                     Name = p.Name,
                                                     Description = p.Description,
                                                     Price = p.Price,
                                                     StockLevel = p.StockLevel,
                                                     Type = TypeDto.Transform(p.Type),
                                                     Material = MaterialDto.Transform(p.Material),
                                                     //Brand = BrandDto.Transform(p.Brand)
                                                 })
                                                 //.Select(p => ProductDto.Transform(p))
                                                 .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/PriceHistory/{id}
        [Route("pricehistory/{id}"), HttpGet]
        public async Task<ActionResult<IEnumerable<PriceHistoryDto>>> GetPriceHistory(int id)
        {
            if (!_context.PriceHistory.Any(e => e.ProductId == id))
            {
                return NotFound();
            }

            return await _context.PriceHistory.Where(p => p.ProductId == id)
                                              .Select(p => PriceHistoryDto.Transform(p))
                                              .ToListAsync();
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(ProductDto.ToProduct(product)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product)
        {
            _context.Products.Add(ProductDto.ToProduct(product));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, ProductDto.ToProduct(product));
        }

        // POST: api/Products/{id}/Purchase
        [HttpPost("api/products/{id}/purchase")]
        public async Task<ActionResult<Product>> PurchaseProduct(int id, OrderDto order)
        {
            if (id != order.Product.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.StockLevel <= 0)
            {
                return BadRequest();
            }

            var createOrder = await _ordersService.CreateOrder(order);

            if (!createOrder)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Products/UpdatePrice/{id}
        [HttpPost("api/products/updateprice/{id}")]
        public async Task<ActionResult<Product>> UpdatePrice(int id, PriceDto price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Price = price.ResalePrice;
            _context.Entry(product).State = EntityState.Modified;

            var priceHistory = new PriceHistory { Price = price.ResalePrice, Product = product };
            _context.PriceHistory.Add(priceHistory);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products/UpdateStock/{id}
        [HttpPost("api/products/updatestock/{id}")]
        public async Task<ActionResult<Product>> UpdateStock(int id, StockDto stock)
        {
            if (id != stock.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.StockLevel += stock.AdditionalStock;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return ProductDto.Transform(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
