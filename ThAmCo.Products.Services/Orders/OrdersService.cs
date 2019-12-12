using System;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        public Task<bool> CreateOrder(OrderDto order)
        {
            return Task.FromResult(true);
        }
    }
}
