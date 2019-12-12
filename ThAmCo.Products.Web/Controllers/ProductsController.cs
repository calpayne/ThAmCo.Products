using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Products.Data;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Orders;
using ThAmCo.Products.Services.Products;

namespace ThAmCo.Products.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _products;
        private readonly IOrdersService _orders;

        public ProductsController(IProductsService products, IOrdersService orders)
        {
            _products = products;
            _orders = orders;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int[] brands, [FromQuery] int[] categories, string term, double? minPrice, double? maxPrice)
        {
            var products = await _products.GetAllAsync(brands, categories, term, minPrice, maxPrice);
            return Ok(products.ToList());
        }

        // GET: api/Products/orderby/stock
        [HttpGet("orderby/stock/")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _products.GetAllByStockAsync();
            return Ok(products.ToList());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _products.GetByIDAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/Products/PriceHistory/{id}
        [Route("pricehistory/{id}"), HttpGet]
        public async Task<IActionResult> GetPriceHistory(int id)
        {
            var product = await _products.GetByIDAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var priceHistory = await _products.GetPriceHistoryAsync(id);

            return Ok(priceHistory.ToList());
        }

        // POST: api/Products/{id}/Purchase
        [HttpPost("api/products/{id}/purchase")]
        public async Task<IActionResult> PurchaseProduct(int id, OrderDto order)
        {
            if (id != order.Product.Id)
            {
                return BadRequest();
            }

            var product = await _products.GetByIDAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.StockLevel <= 0)
            {
                return BadRequest();
            }

            var newStock = product.StockLevel - 1;
            var update = await _products.UpdateProductStockAsync(product.Id, newStock);

            if (!update)
            {
                BadRequest();
            }

            var createOrder = await _orders.CreateOrder(order);

            if (!createOrder)
            {
                // add the stock back since the order wasn't created.
                var newStocku = product.StockLevel + 1;
                var updateu = await _products.UpdateProductStockAsync(product.Id, newStocku);

                return BadRequest();
            }

            return Ok(order);
        }

        // POST: api/Products/UpdatePrice/{id}
        [HttpPost("api/products/updateprice/{id}")]
        public async Task<IActionResult> UpdatePrice(int id, PriceDto price)
        {
            /*
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
            */

            return NoContent();
        }

        // POST: api/Products/UpdateStock/{id}
        [HttpPost("api/products/updatestock/{id}")]
        public async Task<IActionResult> UpdateStock(int id, StockDto stock)
        {
            /*
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
            */

            return NoContent();
        }

        /*
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
        */
    }
}
