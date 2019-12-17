using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Orders;
using ThAmCo.Products.Services.Products;
using ThAmCo.Products.Web.Controllers;

namespace ThAmCo.Products.Tests
{
    [TestClass]
    public class ProductsTests
    {
        [TestMethod]
        public async Task GetAllProducts_ShouldOkObject()
        {
            // Arrange
            IEnumerable<ProductDto> fakeProducts = new List<ProductDto>
            {
                new ProductDto { Id = 1, BrandId = 1, CategoryId = 4, Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = 10.25, StockLevel = 1 },
                new ProductDto { Id = 2, BrandId = 2, CategoryId = 3, Description = "Purchase you favourite chocolate and use the provided heating element to melt it into the perfect cover for your mobile device.", Name = "Chocolate Cover", Price = 50.25, StockLevel = 12 },
                new ProductDto { Id = 3, BrandId = 3, CategoryId = 2, Description = "Lamely adapted used and dirty teatowel.  Guaranteed fewer than two holes.", Name = "Cloth Cover", Price = 100.25, StockLevel = 24 },
                new ProductDto { Id = 4, BrandId = 4, CategoryId = 1, Description = "Especially toughen and harden sponge entirely encases your device to prevent any interaction.", Name = "Harden Sponge Case", Price = 60.25, StockLevel = 4 },
                new ProductDto { Id = 5, BrandId = 1, CategoryId = 4, Description = "Place your device within the water-tight container, fill with water and enjoy the cushioned protection from bumps and bangs.", Name = "Water Bath Case", Price = 20.25, StockLevel = 5 },
                new ProductDto { Id = 6, BrandId = 2, CategoryId = 3, Description = "Keep you smartphone handsfree with this large assembly that attaches to your rear window wiper (Hatchbacks only).", Name = "Smartphone Car Holder", Price = 200.25, StockLevel = 13 },
                new ProductDto { Id = 7, BrandId = 3, CategoryId = 2, Description = "Keep your device on your arm with this general purpose sticky tape.", Name = "Sticky Tape Sport Armband", Price = 20.25, StockLevel = 18 },
                new ProductDto { Id = 8, BrandId = 4, CategoryId = 1, Description = "Stengthen HB pencils guaranteed to leave a mark.", Name = "Real Pencil Stylus", Price = 35.25, StockLevel = 14 },
                new ProductDto { Id = 9, BrandId = 1, CategoryId = 4, Description = "Coat your mobile device screen in a scratch resistant, opaque film.", Name = "Spray Paint Screen Protector", Price = 45.25, StockLevel = 8 },
                new ProductDto { Id = 10, BrandId = 2, CategoryId = 3, Description = "For his or her sensory pleasure. Fits few known smartphones.", Name = "Rippled Screen Protector", Price = 85.25, StockLevel = 5 },
                new ProductDto { Id = 11, BrandId = 3, CategoryId = 2, Description = "For an odour than lingers on your device.", Name = "Fish Scented Screen Protector", Price = 125.25, StockLevel = 3 },
                new ProductDto { Id = 12, BrandId = 4, CategoryId = 1, Description = "Guaranteed not to conduct electical charge from your fingers.", Name = "Non-conductive Screen Protector", Price = 99.25, StockLevel = 0 }
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            var result = await controller.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var productsResult = objResult.Value as IEnumerable<ProductDto>;
            Assert.IsNotNull(productsResult);
            var products = productsResult.ToList();
            Assert.AreEqual(fakeProducts.Count(), products.Count());

            for (int i = 1; i <= products.Count(); i++)
            {
                var real = products.FirstOrDefault(p => p.Id == i);
                var fake = fakeProducts.FirstOrDefault(p => p.Id == i);

                Assert.AreEqual(fake.Id, real.Id);
                Assert.AreEqual(fake.BrandId, real.BrandId);
                Assert.AreEqual(fake.CategoryId, real.CategoryId);
                Assert.AreEqual(fake.Description, real.Description);
                Assert.AreEqual(fake.Name, real.Name);
                Assert.AreEqual(fake.Price, real.Price);
                Assert.AreEqual(fake.StockLevel, real.StockLevel);
            }
        }

        [TestMethod]
        public async Task GetAllProducts_WithValidSearch_ShouldOkObject()
        {
            // Arrange
            IEnumerable<ProductDto> fakeProducts = new List<ProductDto>
            {
                new ProductDto { Id = 5, BrandId = 1, CategoryId = 4, Description = "Place your device within the water-tight container, fill with water and enjoy the cushioned protection from bumps and bangs.", Name = "Water Bath Case", Price = 20.25, StockLevel = 5 },
                new ProductDto { Id = 9, BrandId = 1, CategoryId = 4, Description = "Coat your mobile device screen in a scratch resistant, opaque film.", Name = "Spray Paint Screen Protector", Price = 45.25, StockLevel = 8 },
                new ProductDto { Id = 10, BrandId = 2, CategoryId = 3, Description = "For his or her sensory pleasure. Fits few known smartphones.", Name = "Rippled Screen Protector", Price = 85.25, StockLevel = 5 }
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            int[] brands = { 1, 2 };
            int[] categories = { 4, 3 };

            var result = await controller.GetProducts(brands, categories, "fi", 20, 85.25);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var productsResult = objResult.Value as IEnumerable<ProductDto>;
            Assert.IsNotNull(productsResult);
            var products = productsResult.ToList();
            Assert.AreEqual(fakeProducts.Count(), products.Count());

            for (int i = 0; i < products.Count(); i++)
            {
                Assert.AreEqual(fakeProducts.ElementAt(i).Id, products.ElementAt(i).Id);
                Assert.AreEqual(fakeProducts.ElementAt(i).BrandId, products.ElementAt(i).BrandId);
                Assert.AreEqual(fakeProducts.ElementAt(i).CategoryId, products.ElementAt(i).CategoryId);
                Assert.AreEqual(fakeProducts.ElementAt(i).Description, products.ElementAt(i).Description);
                Assert.AreEqual(fakeProducts.ElementAt(i).Name, products.ElementAt(i).Name);
                Assert.AreEqual(fakeProducts.ElementAt(i).Price, products.ElementAt(i).Price);
                Assert.AreEqual(fakeProducts.ElementAt(i).StockLevel, products.ElementAt(i).StockLevel);
            }
        }

        [TestMethod]
        public async Task GetAllProducts_WithNoResultsSearch_ShouldNoContent()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            int[] brands = { 20 };
            int[] categories = { 20 };

            var result = await controller.GetProducts(brands, categories, "test", 20, 85.25);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NoContentResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task GetProduct_WithValidID_ShouldOkObject()
        {
            // Arrange
            ProductDto fakeProduct = new ProductDto { Id = 1, BrandId = 1, CategoryId = 4, Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = 10.25, StockLevel = 1 };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            var result = await controller.GetProduct(1);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var productResult = objResult.Value as ProductDto;
            Assert.IsNotNull(productResult);
            Assert.AreEqual(fakeProduct.Id, productResult.Id);
            Assert.AreEqual(fakeProduct.BrandId, productResult.BrandId);
            Assert.AreEqual(fakeProduct.CategoryId, productResult.CategoryId);
            Assert.AreEqual(fakeProduct.Description, productResult.Description);
            Assert.AreEqual(fakeProduct.Name, productResult.Name);
            Assert.AreEqual(fakeProduct.Price, productResult.Price);
            Assert.AreEqual(fakeProduct.StockLevel, productResult.StockLevel);
        }

        [TestMethod]
        public async Task GetProduct_WithInvalidID_ShouldNotFound()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            var result = await controller.GetProduct(99999);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task GetPriceHistory_WithValidProduct_ShouldOkObject()
        {
            // Arrange
            IEnumerable<PriceHistoryDto> fakeHistory = new List<PriceHistoryDto>
            {
                new PriceHistoryDto { Id = 1, Price = 10.25, CreatedDate = new DateTime(2019, 1, 18) },
                new PriceHistoryDto { Id = 2, Price = 12.25, CreatedDate = new DateTime(2019, 1, 19) },
                new PriceHistoryDto { Id = 3, Price = 14.25, CreatedDate = new DateTime(2019, 1, 20) },
                new PriceHistoryDto { Id = 4, Price = 16.25, CreatedDate = new DateTime(2019, 1, 21) },
                new PriceHistoryDto { Id = 5, Price = 18.25, CreatedDate = new DateTime(2019, 1, 22) },
                new PriceHistoryDto { Id = 6, Price = 20.25, CreatedDate = new DateTime(2019, 1, 23) }
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            var result = await controller.GetPriceHistory(1);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var priceHistoryResult = objResult.Value as IEnumerable<PriceHistoryDto>;
            Assert.IsNotNull(priceHistoryResult);
            var realHistory = priceHistoryResult.ToList();
            Assert.AreEqual(fakeHistory.Count(), realHistory.Count());

            for (int i = 1; i <= realHistory.Count(); i++)
            {
                var real = realHistory.FirstOrDefault(p => p.Id == i);
                var fake = fakeHistory.FirstOrDefault(p => p.Id == i);

                Assert.AreEqual(fake.Id, real.Id);
                Assert.AreEqual(fake.Price, real.Price);
                Assert.AreEqual(fake.CreatedDate, real.CreatedDate);
            }
        }

        [TestMethod]
        public async Task GetPriceHistory_WithInvalidProduct_ShouldNotFound()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            var result = await controller.GetPriceHistory(99999);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task UpdatePriceHistory_WithValidProduct_ShouldOkObject()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());
            PriceDto price = new PriceDto
            {
                ProductId = 1,
                ResalePrice = 1.95
            };

            // Act
            var result = await controller.UpdatePrice(price);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var priceResult = objResult.Value as PriceDto;
            Assert.IsNotNull(priceResult);
            Assert.AreEqual(priceResult.ProductId, price.ProductId);
            Assert.AreEqual(priceResult.ResalePrice, price.ResalePrice);
        }
        
        [TestMethod]
        public void UpdatePriceHistory_ModelValidate_ShouldBeFalse()
        {
            // Arrange
            var validationResultList = new List<ValidationResult>();
            PriceDto price = new PriceDto
            {
                ProductId = 1,
                ResalePrice = -1
            };

            // Act
            var result = Validator.TryValidateObject(price, new ValidationContext(price), validationResultList, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResultList.Count);
            Assert.AreEqual("ResalePrice", validationResultList[0].MemberNames.ElementAt(0));
            Assert.AreEqual("The field ResalePrice must be between 0.01 and 1.79769313486232E+308.", validationResultList[0].ErrorMessage);
        }

        [TestMethod]
        public async Task UpdatePriceHistory_WithInvalidModelState_ShouldBadRequest()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());
            controller.ModelState.AddModelError("test", "test");
            PriceDto price = new PriceDto
            {
                ProductId = 1,
                ResalePrice = -1
            };

            // Act
            var result = await controller.UpdatePrice(price);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task UpdatePriceHistory_WithInvalidProduct_ShouldNotFound()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());
            PriceDto price = new PriceDto
            {
                ProductId = 9999,
                ResalePrice = 1.00
            };

            // Act
            var result = await controller.UpdatePrice(price);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PurchaseProduct_WithValidProduct_ShouldOkObject()
        {
            // Arrange
            OrderDto order = new OrderDto
            {
                Customer = new CustomerDto(),
                Product = new ProductDto { Id = 1, BrandId = 1, CategoryId = 4, Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = 10.25, StockLevel = 1 }
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act (making order)
            var result = await controller.PurchaseProduct(order);

            // Act (getting product to check stock)
            var result2 = await controller.GetProduct(order.Product.Id);

            // Assert (for making order)
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var orderResult = objResult.Value as OrderDto;
            Assert.IsNotNull(orderResult);
            Assert.AreEqual(orderResult.Product.Id, order.Product.Id);

            // Assert (for getting product to check stock)
            Assert.IsNotNull(result2);
            var objResult2 = result2 as OkObjectResult;
            Assert.IsNotNull(objResult2);
            var productResult = objResult2.Value as ProductDto;
            Assert.IsNotNull(productResult);
            Assert.AreEqual(orderResult.Product.Id, order.Product.Id);
            Assert.AreNotEqual(productResult.StockLevel, order.Product.StockLevel);
            Assert.AreEqual(productResult.StockLevel, (order.Product.StockLevel - 1));
        }

        [TestMethod]
        public async Task PurchaseProduct_WithInvalidProduct_ShouldNotFound()
        {
            // Arrange
            OrderDto order = new OrderDto
            {
                Customer = new CustomerDto(),
                Product = new ProductDto { Id = 9999, BrandId = 1, CategoryId = 4, Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = 10.25, StockLevel = 1 }
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act (making order)
            var result = await controller.PurchaseProduct(order);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PurchaseProduct_WithNoStock_ShouldBadRequest()
        {
            // Arrange
            OrderDto order = new OrderDto
            {
                Customer = new CustomerDto(),
                Product = new ProductDto { Id = 12, BrandId = 4, CategoryId = 1, Description = "Guaranteed not to conduct electical charge from your fingers.", Name = "Non-conductive Screen Protector", Price = 99.25, StockLevel = 0 }
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act (making order)
            var result = await controller.PurchaseProduct(order);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public void PurchaseProduct_ModelValidate_ShouldBeFalse()
        {
            // Arrange
            var validationResultList = new List<ValidationResult>();
            ProductDto product = new ProductDto { Id = 12, BrandId = 4, CategoryId = 1, Description = "Guaranteed not to conduct electical charge from your fingers.", Name = "Non-conductive Screen Protector", Price = 99.25, StockLevel = 0 };
            OrderDto order = new OrderDto
            {
                Product = product
            };

            // Act
            var result = Validator.TryValidateObject(order, new ValidationContext(order), validationResultList, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResultList.Count);
            Assert.AreEqual("Customer", validationResultList[0].MemberNames.ElementAt(0));
            Assert.AreEqual("The Customer field is required.", validationResultList[0].ErrorMessage);
        }

        [TestMethod]
        public async Task PurchaseProduct_WithInvalidModelState_ShouldBadRequest()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());
            controller.ModelState.AddModelError("test", "test");
            ProductDto product = new ProductDto { Id = 12, BrandId = 4, CategoryId = 1, Description = "Guaranteed not to conduct electical charge from your fingers.", Name = "Non-conductive Screen Protector", Price = 99.25, StockLevel = 0 };
            OrderDto order = new OrderDto
            {
                Product = product
            };

            // Act
            var result = await controller.PurchaseProduct(order);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task UpdateProductStock_WithValidProduct_ShouldOkObject()
        {
            // Arrange
            ProductDto product = new ProductDto { Id = 1, BrandId = 1, CategoryId = 4, Description = "Poor quality fake faux leather cover loose enough to fit any mobile device.", Name = "Wrap It and Hope Cover", Price = 10.25, StockLevel = 1 };
            StockDto stock = new StockDto
            {
                ProductId = 1,
                AdditionalStock = 10
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act
            var result = await controller.UpdateStock(stock);
            var result2 = await controller.GetProduct(product.Id);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var stockResult = objResult.Value as StockDto;
            Assert.IsNotNull(stockResult);
            Assert.AreEqual(stockResult.ProductId, stock.ProductId);
            Assert.AreEqual(stockResult.AdditionalStock, stock.AdditionalStock);

            Assert.IsNotNull(result2);
            var objResult2 = result2 as OkObjectResult;
            Assert.IsNotNull(objResult2);
            var productResult = objResult2.Value as ProductDto;
            Assert.IsNotNull(productResult);
            Assert.AreNotEqual(productResult.StockLevel, product.StockLevel);
            Assert.AreEqual(productResult.StockLevel, (product.StockLevel + 10));
        }

        [TestMethod]
        public async Task UpdateProductStock_WithInvalidProduct_ShouldNotFound()
        {
            // Arrange
            StockDto stock = new StockDto
            {
                ProductId = 9999,
                AdditionalStock = 10
            };

            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());

            // Act (making order)
            var result = await controller.UpdateStock(stock);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public void UpdateProductStock_ModelValidate_ShouldBeFalse()
        {
            // Arrange
            var validationResultList = new List<ValidationResult>();
            StockDto stock = new StockDto
            {
                ProductId = 9999,
                AdditionalStock = -1
            };

            // Act
            var result = Validator.TryValidateObject(stock, new ValidationContext(stock), validationResultList, true);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, validationResultList.Count);
            Assert.AreEqual("AdditionalStock", validationResultList[0].MemberNames.ElementAt(0));
            Assert.AreEqual("The field AdditionalStock must be between 1 and 2147483647.", validationResultList[0].ErrorMessage);
        }

        [TestMethod]
        public async Task UpdateProductStock_WithInvalidModelState_ShouldBadRequest()
        {
            // Arrange
            var controller = new ProductsController(new FakeProductsService(), new FakeOrdersService());
            controller.ModelState.AddModelError("test", "test");
            StockDto stock = new StockDto
            {
                ProductId = 9999,
                AdditionalStock = -1
            };

            // Act
            var result = await controller.UpdateStock(stock);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
        }
    }
}
