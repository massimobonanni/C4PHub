using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.StorageAccount.Configurations;
using C4PHub.StorageAccount.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.StorageAccount.Implementations
{
    public class StorageAccountTablePersistance : IC4PPersistance
    {
        private readonly ILogger<StorageAccountTablePersistance> _logger;
        private readonly StorageAccountPersistanceConfiguration _config;

        public StorageAccountTablePersistance(ILogger<StorageAccountTablePersistance> logger,IConfiguration config)
        {
            _logger = logger;
            _config = StorageAccountPersistanceConfiguration.Load(config);
        }

        public async Task<bool> ExistsC4PAsync(C4PInfo c4p, CancellationToken token = default)
        {
            this._logger.LogInformation("Checking if C4P {0} exists", c4p);
            try
            {
                TableServiceClient tableServiceClient = new TableServiceClient(this._config.ConnectionString);
                TableClient tableClient = tableServiceClient.GetTableClient(tableName: this._config.TableName);

                var c4pEntity = await tableClient.GetEntityIfExistsAsync<C4PEntity>(
                    rowKey: c4p.Id,
                    partitionKey: c4p.GeneratePartitionKey()
                );
                this._logger.LogInformation("C4P {0} exists: {1}", c4p, c4pEntity.HasValue);
                return c4pEntity.HasValue;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error during retrieving C4P {0}", c4p);
                throw;
            }
        }

        public async Task<IEnumerable<C4PInfo>> GetOpenedC4PsAsync(CancellationToken token = default)
        {
            var resultList = new List<C4PInfo>();

            TableServiceClient tableServiceClient = new TableServiceClient(this._config.ConnectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: this._config.TableName);

            var now = DateTime.Now.Date;

            var c4pEntities = tableClient.QueryAsync<C4PEntity>(x => x.ExpiredDate>= now);

            await foreach (var entity in c4pEntities)
            {
                resultList.Add(entity.ToC4PInfo());
            }
            
            return resultList;
        }

        public async Task<bool> SaveC4PAsync(C4PInfo c4p, CancellationToken token = default)
        {
            this._logger.LogInformation("Saving C4P {0}", c4p);
            try
            {
                TableServiceClient tableServiceClient = new TableServiceClient(this._config.ConnectionString);
                TableClient tableClient = tableServiceClient.GetTableClient(tableName: this._config.TableName);
                var c4pEntity = new C4PEntity(c4p);
                var response = await tableClient.AddEntityAsync(c4pEntity);
                this._logger.LogInformation("C4P {0} saved: {1}", c4p, !response.IsError);
                return !response.IsError;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error during saving C4P {0}",c4p);
                throw;
            }
        }
    }
}
