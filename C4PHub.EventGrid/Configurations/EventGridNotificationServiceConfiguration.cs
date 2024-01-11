using Microsoft.Extensions.Configuration;

namespace C4PHub.EventGrid.Configurations
{
    internal class EventGridNotificationServiceConfiguration
    {
        const string ConfigRootName = "EventGridNotification";
        public string TopicEndpoint { get; set; }
        public string AccessKey { get; set; }

        public bool Enabled()
        {
            return !string.IsNullOrWhiteSpace(this.TopicEndpoint);
        }

        public static EventGridNotificationServiceConfiguration Load(IConfiguration config)
        {
            var retVal = new EventGridNotificationServiceConfiguration();
            retVal.TopicEndpoint = config[$"{ConfigRootName}:TopicEndpoint"];
            retVal.AccessKey = config[$"{ConfigRootName}:AccessKey"];
            return retVal;
        }

    }
}
