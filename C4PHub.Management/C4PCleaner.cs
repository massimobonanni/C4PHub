using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace C4PHub.Management
{
    public class C4PCleaner
    {
        private readonly ILogger _logger;

        public C4PCleaner(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<C4PCleaner>();
        }

        [Function("C4PCleaner")]
        public void Run([TimerTrigger("%C4PCleanerCron%")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
