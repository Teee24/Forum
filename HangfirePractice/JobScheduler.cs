using Hangfire;
using HangfirePractice.Controllers;

namespace HangfirePractice;

public class JobScheduler
{
    public static void ScheduleRecurringJobs()
    {
        //RecurringJob.AddOrUpdate("在背景運行，抓取股市資訊", () => new WebCrawlerController(null).ScheduleDailyStockInfo(), "0 0/10 * * 1-5 ");
        RecurringJob.AddOrUpdate("在背景運行，抓取股市資訊", () => new WebCrawlerController(null).ScheduleDailyStockInfo(), "0 0/10 9-13 * * 1-5");
    }
}
