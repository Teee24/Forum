using HttpClient_Practice.AdapterHelper;
using HttpClient_Practice.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;

namespace HttpClient_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       private readonly ProductsAdapter _productsAdapter;
        public  ProductsController(ProductsAdapter productsAdapter)
        {
            _productsAdapter = productsAdapter;
        }
        [HttpGet]
        public async Task<IResult> Get()
        {
            var response = await _productsAdapter.GetProductsAsync();

            if (response == null) { }

            return Results.Ok(response);
            //try
            //{
            //    var result = await _productsAdapter.GetProductsAsync();
            //    return Results.Ok(result);
            //    // 處理回應內容
            //}
            //catch (HttpRequestException ex)
            //{
            //    // 處理例外狀況
            //    return Results.BadRequest(new Exception { });
            //} 
            
        }

    }

    
}
