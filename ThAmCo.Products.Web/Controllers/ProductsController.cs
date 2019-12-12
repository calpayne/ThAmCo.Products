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

        // POST: api/Products/Purchase/{id}
        [HttpPost("purchase/{id}")]
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

        // POST: api/Products/UpdatePrice/
        [HttpPost("updateprice/")]
        public async Task<IActionResult> UpdatePrice(PriceDto price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _products.GetByIDAsync(price.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            bool created = await _products.UpdateProductPriceAsync(price);
            
            if (!created)
            {
                return BadRequest();
            }

            return Ok(price);
        }

        // POST: api/Products/UpdateStock/
        [HttpPost("api/products/updatestock/")]
        public async Task<IActionResult> UpdateStock(StockDto stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _products.GetByIDAsync(stock.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            bool updated = await _products.UpdateProductStockAsync(stock);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok(stock);
        }
    }
}
