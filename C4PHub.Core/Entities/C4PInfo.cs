using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Entities
{
    /// <summary>
    /// Represents information about a C4P event.
    /// </summary>
    public class C4PInfo
    {
        /// <summary>
        /// Gets or sets the URL of the event.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the location of the event.
        /// </summary>
        public string EventLocation { get; set; }

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
    }
}
