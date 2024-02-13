using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Requests
{
    /// <summary>
    /// Represents a request to insert or update records in the QuickBase API.
    /// </summary>
    public class InsertOrUpdateRecordRequest
    {
        /// <summary>
        /// Gets or sets the ID or name of the table to which records will be inserted or updated.
        /// </summary>
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the data to be inserted or updated, represented as a list of dictionaries mapping field IDs to field values.
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, FieldValue>> Data { get; set; } = new List<Dictionary<string, FieldValue>>();

        /// <summary>
        /// Gets or sets the IDs of fields to return after the insert or update operation.
        /// </summary>
        [JsonProperty("fieldsToReturn", NullValueHandling = NullValueHandling.Ignore)]
        public int[] FieldsToReturn { get; set; } = Array.Empty<int>();
    }
}