using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Responses
{
    /// <summary>
    /// Represents a response from a record update operation in the QuickBase API.
    /// </summary>
    public class QuickbaseRecordUpdateResponse
    {
        /// <summary>
        /// Gets or sets the data updated in the record.
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, FieldValue>> Data { get; set; } = new List<Dictionary<string, FieldValue>>();

        /// <summary>
        /// Gets or sets the metadata associated with the response.
        /// </summary>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; } = new Metadata();
    }
}