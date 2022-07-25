using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace AzFuncIsol
{
    public class TimerTriggeredFunction
    {
        private readonly ILogger _logger;

        public TimerTriggeredFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimerTriggeredFunction>();
        }

        [Function("TimerTriggeredFunction")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            DirectoryInfo assemblyDirectory = new DirectoryInfo(AppContext.BaseDirectory);
            Console.WriteLine(assemblyDirectory.FullName);
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev");
            Console.WriteLine($"page title is {await page.TitleAsync()}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
