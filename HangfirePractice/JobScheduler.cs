using Hangfire;
using HangfirePractice.Controllers;

namespace HangfirePractice;

public static class JobScheduler
{

    public static void ScheduleRecurringJobs()
    {
        // w1~w5的9:00~12:00，每5分鐘一次
        RecurringJob.AddOrUpdate(
            "在背景運行，定時取得當日股市資訊(9-12)",
            () => new WebCrawlerController(null).GetStockInfoBySchedule(),
            "*/5 9-12 * * 1-5",
            TimeZoneInfo.Local);
        // w1~w5的13:00-13:30，每5分鐘一次
        RecurringJob.AddOrUpdate(
            "在背景運行，定時取得當日股市資訊(1300-1330)",
            () => new WebCrawlerController(null).GetStockInfoBySchedule(),
            "0-30/5 13 * * 1-5",
            TimeZoneInfo.Local);

        RecurringJob.AddOrUpdate(
            "定時清除當日股市資訊，將當日股市資訊存入資料庫",
            () => new WebCrawlerController(null).SaveStockInfo(StockDataCache.GetAndClearData()),
            "35 13 * * 1-5",
            TimeZoneInfo.Local);
    }
}
