using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Responses
{
    public class QuickbaseRecordUpdateResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, FieldValue>> Data { get; set; } = new List<Dictionary<string, FieldValue>>();

        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; } = new Metadata();
    }
}