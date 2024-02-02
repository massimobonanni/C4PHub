using System;
using C4PHub.Core.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace C4PHub.Management
{
    public class C4PCleaner
    {
        private readonly ILogger _logger;
        private readonly IC4PPersistance _persistance;
        private readonly IConfiguration _config;

        public C4PCleaner(ILoggerFactory loggerFactory, IConfiguration configuration,
            IC4PPersistance persistance)
        {
            _logger = loggerFactory.CreateLogger<C4PCleaner>();
            _persistance = persistance;
            _config = configuration;
        }

        [Function("C4PCleaner")]
        public async Task Run([TimerTrigger("%C4PCleanerCron%")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C4PCleaner function executed at: {DateTime.Now}");
            
            var c4pOlderThan=_config.GetValue<int>("C4PCleanerExpiredOlderThanDays");
            
            // Retrieve all closed C4Ps
            var closedC4Ps = await _persistance.GetClosedC4PsAsync();
            // Only keep C4Ps that are older than X days
            closedC4Ps=closedC4Ps.Where(c4p => c4p.ExpiredDate < DateTime.Now.AddDays(-c4pOlderThan));

            foreach (var c4p in closedC4Ps)
            {
                // Delete C4P
                var deleteResult=await _persistance.DeleteC4PAsync(c4p);

                if (deleteResult)
                {
                    _logger.LogWarning("C4P {0} deleted",c4p);
                }
                else
                {
                    _logger.LogError("C4P {0} could not be deleted",c4p);
                }
            }
        }
    }
}
