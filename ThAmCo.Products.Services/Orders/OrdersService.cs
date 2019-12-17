using System;
using System.Net;
using System.Net.Http;
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
                HttpResponseMessage response = await _client.PostAsJsonAsync("/api/purchase/", order);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                response.EnsureSuccessStatusCode();

                has = await response.Content.ReadAsAsync<bool>();
            }
            catch (HttpRequestException)
            {
                has = false;
            }

            return has;
        }
    }
}
