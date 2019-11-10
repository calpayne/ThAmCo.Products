using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Orders
{
    public interface IOrdersService
    {
        Task<bool> CreateOrder(OrderDto order);
    }
}
