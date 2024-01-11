using Azure.Messaging.EventGrid;
using Azure;
using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.EventGrid.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.Serialization;
using System.Text.Json;
using C4PHub.EventGrid.Entities;

namespace C4PHub.EventGrid.Implementations
{
    public class EventGridNotificationService : INotificationService
    {
        private readonly ILogger<EventGridNotificationService> _logger;
        private readonly EventGridNotificationServiceConfiguration _config;

        public EventGridNotificationService(ILogger<EventGridNotificationService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = EventGridNotificationServiceConfiguration.Load(config);
        }

        public async Task SendNotificationAsync(C4PInfo c4pInfo, CancellationToken cancellationToken = default)
        {
            if (this._config.Enabled())
            {
                this._logger.LogInformation("Sending notification for C4P {0}", c4pInfo);
                try
                {
                    EventGridPublisherClient client = new EventGridPublisherClient(new Uri(this._config.TopicEndpoint),
                                new AzureKeyCredential(this._config.AccessKey));

                    var dataSerializer = new JsonObjectSerializer(
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                    var @event = new EventGridEvent(c4pInfo.EventName, "C4PHub.Events.C4PAdded", "1.0",
                        dataSerializer.Serialize(new EventData(c4pInfo)));
                    await client.SendEventAsync(@event);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex,"Error sending notification for C4P {0}",c4pInfo);
                }
            }
            else
            {
                this._logger.LogInformation("Notification for C4P {0} is disabled", c4pInfo);
            }
        }
    }
}
