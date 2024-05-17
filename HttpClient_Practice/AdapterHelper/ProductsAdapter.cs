using HttpClient_Practice.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace HttpClient_Practice.AdapterHelper;

public class ProductsAdapter
{
    private readonly HttpClient _httpClient;
    public ProductsAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/products");


        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    }


    public async Task<IEnumerable<Product.Class1>?> GetProductsAsync()
    {

        var responseString = await _httpClient.GetFromJsonAsync<IEnumerable<Product.Class1>>("/?categoryId=2");
        return responseString;
    }
}
