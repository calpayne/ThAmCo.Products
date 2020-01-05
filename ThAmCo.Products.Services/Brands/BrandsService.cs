using Microsoft.Extensions.Configuration;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using ThAmCo.Products.Models;

namespace ThAmCo.Products.Services.Brands
{
    public class BrandsService : IBrandsService
    {
        private readonly HttpClient _client;

        public BrandsService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            IEnumerable<BrandDto> brands;

            try
            {
                HttpResponseMessage response = await _client.GetAsync("Brand");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                response.EnsureSuccessStatusCode();
                brands = await response.Content.ReadAsAsync<IEnumerable<BrandDto>>();
            }
            catch (SocketException)
            {
                brands = Array.Empty<BrandDto>();
            }
            catch (BrokenCircuitException)
            {
                brands = Array.Empty<BrandDto>();
            }
            catch (HttpRequestException)
            {
                brands = Array.Empty<BrandDto>();
            }
            catch (UnsupportedMediaTypeException)
            {
                brands = Array.Empty<BrandDto>();
            }

            return brands;
        }

        public async Task<BrandDto> GetByIDAsync(int id)
        {
            BrandDto brand;

            try
            {
                HttpResponseMessage response = await _client.GetAsync("Brand/" + id);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                response.EnsureSuccessStatusCode();
                brand = await response.Content.ReadAsAsync<BrandDto>();
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
            catch (UnsupportedMediaTypeException)
            {
                return null;
            }

            return brand;
        }
    }
}
