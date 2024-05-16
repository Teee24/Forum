using HttpClient_Practice.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace HttpClient_Practice.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class QuotesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public QuotesController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        //基本使用方式
        [HttpGet]
        public async Task<IResult> GetQuotes()
        {
            string uri = "https://favqs.com/api/quotes/";
            string querystring = "";
            string token = "e3f21f68737d1cf1fb53f3aa38c8218d";
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            HttpResponseMessage response = await httpClient.GetAsync(querystring);
         
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var responseString = JsonConvert.DeserializeObject<RootObject>(resultString);

                return Results.Ok(responseString);
            }
            return Results.BadRequest();
        }
    }
}
