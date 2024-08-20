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
    public async Task<bool> SaveETFInfo(List<ETFInfo> targets)
    {
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

        // 建立連線
        using var conn = ForumConnection();

        // 使用事務來確保所有插入操作的原子性
        using var transaction = conn.BeginTransaction();

        try
        {
            // 使用 Dapper 的 ExecuteAsync 方法批次執行插入操作
            var count = await conn.ExecuteAsync(sql, targets, transaction);

            // 提交事務
            transaction.Commit();

            if (count <= 0)
            {
                return false;
            }

            //Console.WriteLine("Ok");
            return true;
        }
        catch (Exception ex)
        {
            // 若有錯誤，回滾事務
            transaction.Rollback();
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }


    }

    public async Task<ETFInfo> GetETFInfoCurrent(string key)
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

        string date = DateTime.Now.ToString("yyyy/MM/dd");
        string time = DateTime.Now.ToString("HH:mm:ss");

        etfInfo.Date = date;
        etfInfo.Time = time;
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

    public async Task<IEnumerable<ETFInfo>> GetETFInfoPast(string key, string date)
    {
        string sql = @"SELECT [Date]
                              ,[Time]
                              ,[TargetName]
                              ,[TargetCode]
                              ,[ClosingPrice]
                              ,[OpeningPrice]
                              ,[HighestPrice]
                              ,[LowestPrice]
                              ,[AveragePrice]
                              ,[TotalTradingValueBillion]
                              ,[PreviousClosingPrice]
                              ,[PriceChangePercentage]
                              ,[PriceChange]
                              ,[TotalVolume]
                              ,[PreviousVolume]
                              ,[PriceFluctuationPercentage]
                          FROM [Forum].[dbo].[ETFInfo]
                          WHERE 1 = 1";

        if (string.IsNullOrEmpty(key))
            sql += " AND [Date] = @Date";

        if (string.IsNullOrEmpty(date))
            sql += " AND [TargetCode] = @TargetCode";

        using var conn = ForumConnection();

        var result = await conn.QueryAsync<ETFInfo>(sql, new { Date = date, TargetCode = key });

        return result;
    }
}
