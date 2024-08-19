using AngleSharp.Io;
using AngleSharp;
using Microsoft.Data.SqlClient;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Dapper;
using HangfirePractice.Model;

namespace HangfirePractice.Service;

public class StockService : IStockService
{
    private readonly IConfiguration _configuration;

    public StockService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // 建立 SQL 連接的方法
    private SqlConnection ForumConnection() => new SqlConnection(_configuration.GetConnectionString("Forum"));
    public async Task<bool> GetAndSaveETFInfo(string target)
    {
        var etfInfo = await GetETFInfo(target);

        string sql = @"
            INSERT INTO [Forum].[dbo].[ETFInfo]
            (
                [Date],
                [Time],
                [TargetName],
                [TargetCode],
                [ClosingPrice],
                [OpeningPrice],
                [HighestPrice],
                [LowestPrice],
                [AveragePrice],
                [TotalTradingValueBillion],
                [PreviousClosingPrice],
                [PriceChangePercentage],
                [PriceChange],
                [TotalVolume],
                [PreviousVolume],
                [PriceFluctuationPercentage]
            )
            VALUES
            (
                @Date,
                @Time,
                @TargetName,
                @TargetCode,
                @ClosingPrice,
                @OpeningPrice,
                @HighestPrice,
                @LowestPrice,
                @AveragePrice,
                @TotalTradingValueBillion,
                @PreviousClosingPrice,
                @PriceChangePercentage,
                @PriceChange,
                @TotalVolume,
                @PreviousVolume,
                @PriceFluctuationPercentage
            );";

        using var conn = ForumConnection();

        string date = DateTime.Now.ToString("yyyy/MM/dd");
        string time = DateTime.Now.ToString("HH:mm:ss");

        var parameters = new
        {
            Date = date, // 現在時間
            time = time,
            etfInfo.TargetName,
            etfInfo.TargetCode,
            etfInfo.ClosingPrice,
            etfInfo.OpeningPrice,
            etfInfo.HighestPrice,
            etfInfo.LowestPrice,
            etfInfo.AveragePrice,
            etfInfo.TotalTradingValueBillion,
            etfInfo.PreviousClosingPrice,
            etfInfo.PriceChangePercentage,
            etfInfo.PriceChange,
            etfInfo.TotalVolume,
            etfInfo.PreviousVolume,
            etfInfo.PriceFluctuationPercentage
        };

        var count = await conn.ExecuteAsync(sql, parameters);

        if (count <= 0)
        {
            return false;
        }
        return true;


    }

    public async Task<ETFInfo> GetETFInfo(string key)
    {

        // 建立 Browser 的配置
        var config = Configuration.Default.WithDefaultLoader(
        new LoaderOptions
        {
            IsResourceLoadingEnabled = true
        });

        // 根據配置建立出我們的 Browser 
        var browser = BrowsingContext.New(config);

        string url = $"https://tw.stock.yahoo.com/quote/{key}";

        var document = await browser.OpenAsync(url);
        var tables = document?.QuerySelectorAll("li.price-detail-item");
        var title = document?.QuerySelector("h1[class^='C($c-link-text)']");
        document.Close();

        var etfInfo = new ETFInfo();

        etfInfo.TargetName = title?.TextContent;
        etfInfo.TargetCode = key;

        foreach (var item in tables)
        {
            switch (item.FirstChild.TextContent)
            {
                case "成交":
                    etfInfo.ClosingPrice = item.LastChild.TextContent;
                    break;
                case "開盤":
                    etfInfo.OpeningPrice = item.LastChild.TextContent;
                    break;
                case "最高":
                    etfInfo.HighestPrice = item.LastChild.TextContent;
                    break;
                case "最低":
                    etfInfo.LowestPrice = item.LastChild.TextContent;
                    break;
                case "均價":
                    etfInfo.AveragePrice = item.LastChild.TextContent;
                    break;
                case "成交金額(億)":
                    etfInfo.TotalTradingValueBillion = item.LastChild.TextContent;
                    break;
                case "昨收":
                    etfInfo.PreviousClosingPrice = item.LastChild.TextContent;
                    break;
                case "漲跌幅":
                    etfInfo.PriceChangePercentage = item.LastChild.TextContent;
                    break;
                case "漲跌":
                    etfInfo.PriceChange = item.LastChild.TextContent;
                    break;
                case "總量":
                    etfInfo.TotalVolume = item.LastChild.TextContent;
                    break;
                case "昨量":
                    etfInfo.PreviousVolume = item.LastChild.TextContent;
                    break;
                case "振幅":
                    etfInfo.PriceFluctuationPercentage = item.LastChild.TextContent;
                    break;
                default:
                    // Handle unknown cases if necessary
                    break;
            }
        }

        return etfInfo;
    }
}
