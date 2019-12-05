using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;
using ThAmCo.Products.Services.Brands;
using ThAmCo.Products.Web.Controllers;

namespace ThAmCo.Products.Tests
{
    [TestClass]
    public class BrandsTests
    {
        [TestMethod]
        public async Task GetAllBrands_ShouldOkObject()
        {
            // Arrange
            IEnumerable<BrandDto> fakeBrands = new List<BrandDto>
            {
                new BrandDto { Id = 1, Name = "Soggy Sponge" },
                new BrandDto { Id = 2, Name = "Damp Squib" },
                new BrandDto { Id = 3, Name = "iStuff-R-Us" }
            };

            var controller = new BrandsController(new FakeBrandsService());

            // Act
            var result = await controller.GetBrands();

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var brandsResult = objResult.Value as IEnumerable<BrandDto>;
            Assert.IsNotNull(brandsResult);
            var brands = brandsResult.ToList();
            Assert.AreEqual(fakeBrands.Count(), brands.Count());

            for (int i = 1; i <= brands.Count(); i++)
            {
                var real = brands.FirstOrDefault(p => p.Id == i);
                var fake = fakeBrands.FirstOrDefault(p => p.Id == i);

                Assert.AreEqual(fake.Id, real.Id);
                Assert.AreEqual(fake.Name, real.Name);
            }
        }

        [TestMethod]
        public async Task GetBrand_WithValidID_ShouldOkObject()
        {
            // Arrange
            BrandDto fakeBrand = new BrandDto { Id = 1, Name = "Soggy Sponge" };

            var controller = new BrandsController(new FakeBrandsService());

            // Act
            var result = await controller.GetBrand(1);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var brandResult = objResult.Value as BrandDto;
            Assert.IsNotNull(brandResult);
            Assert.AreEqual(fakeBrand.Id, brandResult.Id);
            Assert.AreEqual(fakeBrand.Name, brandResult.Name);
        }

        [TestMethod]
        public async Task GetBrand_WithInvalidID_ShouldNotFound()
        {
            // Arrange
            var controller = new BrandsController(new FakeBrandsService());

            // Act
            var result = await controller.GetBrand(99999);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }
    }
}
