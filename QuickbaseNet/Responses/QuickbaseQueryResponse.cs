using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Responses
{
    /// <summary>
    /// Represents a response from a query operation in the QuickBase API.
    /// </summary>
    public class QuickbaseQueryResponse
    {
        /// <summary>
        /// Gets or sets the data retrieved from the query.
        /// </summary>
        [JsonProperty("data")]
        public List<Dictionary<string, FieldValue>> Data { get; set; }

        /// <summary>
        /// Gets or sets the fields included in the response.
        /// </summary>
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }

        /// <summary>
        /// Gets or sets the metadata associated with the response.
        /// </summary>
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}