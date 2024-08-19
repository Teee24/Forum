using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Hangfire.Common;
using HangfirePractice.Model;

namespace HangfirePractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] TransportationModes = new[]
                                {
                                    "汽車",
                                    "公共汽車",
                                    "地鐵",
                                    "自行車",
                                    "步行"
                                };

        [HttpGet(Name = "GetTransportation")]
        public IEnumerable<Transportation> GetTransportation()
        {
            //單次立即執行
            BackgroundJob.Enqueue(() => Console.WriteLine("單次!"));
            //單次10秒後執行
            BackgroundJob.Schedule(() => Console.WriteLine("10秒後執行!"), TimeSpan.FromSeconds(10));
            //重複執行，預設為每天00:00啟動
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Today is the day!!!"), Cron.Monthly());

            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Hello, "));
            BackgroundJob.ContinueWith(id, () => Console.WriteLine("world!"));

            return Enumerable.Range(1, 5).Select(index => new Transportation
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                SpendTime = new Random().Next(),
                Summary = TransportationModes[Random.Shared.Next(TransportationModes.Length)]
            })
            .ToArray();
        }


    }
}
