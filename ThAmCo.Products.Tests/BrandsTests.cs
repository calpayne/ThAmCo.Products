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
    public class BrandsTests
    {
        [TestMethod]
        public async Task GetAllProducts_ShouldOkObject()
        {
            // Arrange
            IEnumerable<BrandDto> fakeBrands = new List<BrandDto>
            {
                
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
    }
}
