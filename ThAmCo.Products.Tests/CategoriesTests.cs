using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Categories;
using ThAmCo.Products.Web.Controllers;

namespace ThAmCo.Products.Tests
{
    [TestClass]
    public class CategoriesTests
    {
        [TestMethod]
        public async Task GetAllCategories_ShouldOkObject()
        {
            // Arrange
            IEnumerable<CategoryDto> fakeCategories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Covers", Description = "Davison Stores pride ourselves on our poor range of covers for your mobile device at premium prices.  If you're lukcy your phone or tablet will be protected from any dents, scratches and scuffs." },
                new CategoryDto { Id = 2, Name = "Case", Description = "Browse our wide range of cases for phones and tablets that will help you to keep your mobile device protected from the elements." },
                new CategoryDto { Id = 3, Name = "Accessories", Description = "We stock a small range of phone and tablet accessories, including car holders, sports armbands, stylus pens and very little else." },
                new CategoryDto { Id = 4, Name = "Screen Protectors", Description = "Exclusive Davison Stores screen protectors for your phone or tablet." }
            };

            var controller = new CategoriesController(new FakeCategoriesService());

            // Act
            var result = await controller.GetCategories();

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var categoriesResult = objResult.Value as IEnumerable<CategoryDto>;
            Assert.IsNotNull(categoriesResult);
            var categories = categoriesResult.ToList();
            Assert.AreEqual(fakeCategories.Count(), categories.Count());

            for (int i = 1; i <= categories.Count(); i++)
            {
                var real = categories.FirstOrDefault(p => p.Id == i);
                var fake = fakeCategories.FirstOrDefault(p => p.Id == i);

                Assert.AreEqual(fake.Id, real.Id);
                Assert.AreEqual(fake.Name, real.Name);
                Assert.AreEqual(fake.Description, real.Description);
            }
        }

        [TestMethod]
        public async Task GetCategory_WithValidID_ShouldOkObject()
        {
            // Arrange
            CategoryDto fakeCategory = new CategoryDto { Id = 1, Name = "Covers", Description = "Davison Stores pride ourselves on our poor range of covers for your mobile device at premium prices.  If you're lukcy your phone or tablet will be protected from any dents, scratches and scuffs." };

            var controller = new CategoriesController(new FakeCategoriesService());

            // Act
            var result = await controller.GetCategory(1);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var categoryResult = objResult.Value as CategoryDto;
            Assert.IsNotNull(categoryResult);
            Assert.AreEqual(fakeCategory.Id, categoryResult.Id);
            Assert.AreEqual(fakeCategory.Name, categoryResult.Name);
            Assert.AreEqual(fakeCategory.Description, categoryResult.Description);
        }

        [TestMethod]
        public async Task GetCategory_WithInvalidID_ShouldNotFound()
        {
            // Arrange
            var controller = new CategoriesController(new FakeCategoriesService());

            // Act
            var result = await controller.GetCategory(99999);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }
    }
}
