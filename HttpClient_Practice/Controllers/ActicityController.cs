﻿using HttpClient_Practice.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;  //配合JsonConvert
using System.Net.Http.Headers;
//using System.Text.Json;

namespace HttpClient_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActicityController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ActicityController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [HttpGet]
        public async Task<IResult> GetActivity([FromQuery] ActivityRequest? request)
        {
            string querystring = "";
            if (request != null)
            {
                querystring = $"?type={request.type}&filter={request.filter}";
            }
            var httpClient = _httpClientFactory.CreateClient("Acticities");
            HttpResponseMessage response = await httpClient.GetAsync(querystring);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var responseString = JsonConvert.DeserializeObject<Activities.ActivityRootobject>(resultString);

                return Results.Ok(responseString);
            }
            return Results.BadRequest();
        }
    }
}
