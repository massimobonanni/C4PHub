using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.StorageAccount.Configurations
{
    internal class StorageAccountPersistanceConfiguration
    {
        const string ConfigRootName = "StorageAccount";
        public string ConnectionString { get; set; }
        public string TableName { get; set; }

        public static StorageAccountPersistanceConfiguration Load(IConfiguration config)
        {
            var retVal = new StorageAccountPersistanceConfiguration();
            retVal.ConnectionString = config[$"{ConfigRootName}:ConnectionString"];
            retVal.TableName = config[$"{ConfigRootName}:TableName"];
            return retVal;
        }

    }
}
