using Newtonsoft.Json;

namespace QuickbaseNet.Requests
{
    /// <summary>
    /// Represents a request to delete records in the QuickBase API.
    /// </summary>
    public class DeleteRecordRequest
    {
        /// <summary>
        /// Gets or sets the ID or name of the table from which to delete records.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the WHERE clause specifying which records to delete.
        /// </summary>
        [JsonProperty("where")]
        public string Where { get; set; }
    }
}