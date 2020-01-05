using Polly.CircuitBreaker;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient _client;

        public OrdersService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> CreateOrder(OrderDto order)
        {
            bool has;

            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("/api/orders/purchase/", order);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                response.EnsureSuccessStatusCode();

                OrderDto data = await response.Content.ReadAsAsync<OrderDto>();

                has = data != null && data.Product.Id == order.Product.Id && data.Customer.Id == order.Customer.Id;
            }
            catch (SocketException)
            {
                has = false;
            }
            catch (BrokenCircuitException)
            {
                has = false;
            }
            catch (HttpRequestException)
            {
                has = false;
            }

            return has;
        }
    }
}
