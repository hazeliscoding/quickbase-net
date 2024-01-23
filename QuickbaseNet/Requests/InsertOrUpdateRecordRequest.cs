using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Requests
{
    public class InsertOrUpdateRecordRequest
    {
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public string To { get; set; } = string.Empty;

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, FieldValue>> Data { get; set; } = new List<Dictionary<string, FieldValue>>();

        [JsonProperty("fieldsToReturn", NullValueHandling = NullValueHandling.Ignore)]
        public int[] FieldsToReturn { get; set; } = Array.Empty<int>();
    }
}