using AngleSharp.Io;
using AngleSharp;
using Hangfire;
using HangfirePractice.Model;
using HangfirePractice.Service;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HangfirePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebCrawlerController : ControllerBase
    {
        private readonly IStockService _stockService;

        public WebCrawlerController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("schedule")]
        public IResult ScheduleDailyStockInfo()
        {

            var stocks = new List<string> { "2317", "2330", "00929", "2454", "2607", "00919", "00878", "2230", "00713", "006205" };

            foreach (var stock in stocks)
            {
                _stockService.GetAndSaveETFInfo(stock);
                Console.WriteLine($"Finding Stock: {stock}......");
            }

            return Results.Ok("Daily stock info job scheduled.");
        }

        [HttpGet("key")]
        public async Task<ETFInfo> GetStockInfo(string key)
        {
            BackgroundJob.Enqueue(() => Console.Write("Start Collect Info..."));
            var result = await _stockService.GetETFInfo(key);

            return result;
        }

        //[HttpGet(Name = "GetNew")]
        //public async Task<IEnumerable<string>> GetExchangeRatesInfo()
        //{
        //    // 建立 Browser 的配置
        //    var config = Configuration.Default.WithDefaultLoader();

        //    // 根據配置建立出我們的 Browser 
        //    var browser = BrowsingContext.New(config);

        //    string url = "https://tw.news.yahoo.com/world/";

        //    var document = await browser.OpenAsync(url);
        //    var tableRows = document.QuerySelectorAll("div h3 a");

        //    // 關閉文檔
        //    document.Close();

        //    var result = tableRows.Select(x => x.QuerySelector(""));

        //    return result;
        //}


    }

}
