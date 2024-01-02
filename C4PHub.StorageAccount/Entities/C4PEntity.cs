using Azure;
using Azure.Data.Tables;
using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.StorageAccount.Entities
{
    internal class C4PEntity : ITableEntity
    {
        public C4PEntity(C4PInfo c4p)
        {
            PartitionKey = c4p.GeneratePartitionKey();
            RowKey = c4p.GenerateUniqueID();
            
            Url = c4p.Url;
            EventName = c4p.EventName;
            EventLocation = c4p.EventLocation;
            EventDate = c4p.EventDate.ToUniversalTime();
            ExpiredDate = c4p.ExpiredDate.ToUniversalTime();
            UserPublished = c4p.UserPublished;
        }

        public C4PEntity()
        {

        }

        public string PartitionKey { get; set; } = default!;
        public string RowKey { get; set; } = default!;
        public DateTimeOffset? Timestamp { get; set; } = default!;
        public ETag ETag { get; set; } = default!;

        /// <summary>
        /// Gets or sets the URL of the event.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string? EventName { get; set; }

        /// <summary>
        /// Gets or sets the location of the event.
        /// </summary>
        public string? EventLocation { get; set; }

        /// <summary>
        /// Gets or sets the date of the event.
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Gets or sets the expired date of the event.
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Gets or sets the user who published the event.
        /// </summary>
        public string UserPublished { get; set; }

        /// <summary>
        /// Converts the C4PEntity object to a C4PInfo object.
        /// </summary>
        /// <returns>The converted C4PInfo object.</returns>
        public C4PInfo ToC4PInfo()
        {
            return new C4PInfo()
            {
                Url = this.Url,
                EventName = this.EventName,
                EventLocation = this.EventLocation,
                EventDate = this.EventDate,
                ExpiredDate = this.ExpiredDate,
                UserPublished = this.UserPublished
            };
        }
    }

}
