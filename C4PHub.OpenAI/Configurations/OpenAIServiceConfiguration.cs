using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.OpenAI.Configurations
{
    internal class OpenAIServiceConfiguration
    {
        const string ConfigRootName = "OpenAIService";
        public string Key { get; set; }
        public string Endpoint { get; set; }
        public string ModelName { get; set; }

        public static OpenAIServiceConfiguration Load(IConfiguration config)
        {
            var retVal = new OpenAIServiceConfiguration();
            retVal.Key = config[$"{ConfigRootName}:Key"];
            retVal.Endpoint = config[$"{ConfigRootName}:Endpoint"];
            retVal.ModelName = config[$"{ConfigRootName}:ModelName"];
            return retVal;
        }

    }
}
