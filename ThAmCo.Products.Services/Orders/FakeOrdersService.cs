using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Orders
{
    public class FakeOrdersService : IOrdersService
    {
        public Task<bool> CreateOrder(OrderDto order)
        {
            return Task.FromResult(true);
        }
    }
}
