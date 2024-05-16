using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace HttpClient_Practice.Request
{
    public class ProductsServer
    {
        private readonly HttpClient _httpClient;
        public ProductsServer(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/products");

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }

        [HttpGet]
        public async Task<IEnumerable<Product>?> GetProductsAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("");
    }
}
