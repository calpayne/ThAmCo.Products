using Microsoft.Extensions.Configuration;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient _client;

        public CategoriesService(IConfiguration config, HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categories;

            try
            {
                HttpResponseMessage response = await _client.GetAsync("Category");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                response.EnsureSuccessStatusCode();

                categories = await response.Content.ReadAsAsync<IEnumerable<CategoryDto>>();
            }
            catch (SocketException)
            {
                categories = Array.Empty<CategoryDto>();
            }
            catch (BrokenCircuitException)
            {
                categories = Array.Empty<CategoryDto>();
            }
            catch (HttpRequestException)
            {
                categories = Array.Empty<CategoryDto>();
            }

            return categories;
        }

        public async Task<CategoryDto> GetByIDAsync(int id)
        {
            CategoryDto category;

            try
            {
                HttpResponseMessage response = await _client.GetAsync("Category/" + id);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                response.EnsureSuccessStatusCode();

                category = await response.Content.ReadAsAsync<CategoryDto>();
            }
            catch (SocketException)
            {
                return null;
            }
            catch (BrokenCircuitException)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }

            return category;
        }
    }
}
