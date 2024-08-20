using AngleSharp.Io;
using AngleSharp;
using Hangfire;
using HangfirePractice.Model;
using HangfirePractice.Service;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using HangfirePractice.Models;

namespace HangfirePractice.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WebCrawlerController : ControllerBase
    {
        private readonly IStockService _stockService;

        public WebCrawlerController(IStockService stockService)
        {
            _stockService = stockService;
        }
        /// <summary>
        /// 取得即時股票資訊
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("key")]
        public async Task<ETFInfo> GetStockInfo(string key)
        {
            BackgroundJob.Enqueue(() => Console.Write("Start Collect Info.../r/n"));
            var result = await _stockService.GetETFInfoCurrent(key);

            return result;
        }

        /// <summary>
        /// 排程每5分鐘取得即時資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ETFInfo[]> GetStockInfoBySchedule()
        {

            var stocks = new List<string> { "2317", "2330", "00929", "2454", "2607", "00919", "00878", "2230", "00713", "006205" };

            var tasks = stocks.Select(async s => await _stockService.GetETFInfoCurrent(s)).ToList();

            var result = await Task.WhenAll(tasks);
            StockDataCache.AddData(result);

            return result;
        }

        /// <summary>
        /// 排程存入資料庫
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SaveStockInfo(List<ETFInfo> targets)
        {
            var result = await _stockService.SaveETFInfo(targets);

            return result;
        }

        /// <summary>
        /// 目前站存的資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ETFInfo>> GetStockInfos()
        {

            var result = StockDataCache.GetData();
            var currentCacheSize = StockDataCache.GetData().Count;
            Console.WriteLine($"Current cache size: {currentCacheSize}");
            return result;
        }

        /// <summary>
        /// 歷史資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ETFInfo>> GetHistoryStockInfos([FromQuery] SearchStockRequest request)
        {

            var etfInfos = await _stockService.GetETFInfoPast(request.StockCode, request.Date);

            var result = etfInfos.Select(x => new ETFInfo
            {
                Date = x.Date,
                Time = x.Time,
                TargetName = x.TargetName,
                TargetCode = x.TargetCode,
                ClosingPrice = x.ClosingPrice,
                OpeningPrice = x.OpeningPrice,
                HighestPrice = x.HighestPrice,
                LowestPrice = x.LowestPrice,
                AveragePrice = x.AveragePrice,
                TotalTradingValueBillion = x.TotalTradingValueBillion,
                PreviousClosingPrice = x.PreviousClosingPrice,
                PriceChangePercentage = x.PriceChangePercentage,
                PriceChange = x.PriceChange,
                TotalVolume = x.TotalVolume,
                PreviousVolume = x.PreviousVolume,
                PriceFluctuationPercentage = x.PriceFluctuationPercentage
            }).ToList();


            return result;
        }
    }

}
