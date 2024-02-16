using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    /// <summary>
    /// Represents metadata associated with a QuickBase response.
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Gets or sets the total number of records.
        /// </summary>
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the number of records returned.
        /// </summary>
        [JsonProperty("numRecords")]
        public int NumRecords { get; set; }

        /// <summary>
        /// Gets or sets the number of fields returned.
        /// </summary>
        [JsonProperty("numFields")]
        public int NumFields { get; set; }

        /// <summary>
        /// Gets or sets the number of records to skip.
        /// </summary>
        [JsonProperty("skip")]
        public int Skip { get; set; }
    }
}