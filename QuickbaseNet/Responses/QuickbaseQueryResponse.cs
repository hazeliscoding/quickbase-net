using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Responses
{
    public class QuickbaseQueryResponse
    {
        [JsonProperty("data")]
        public List<Dictionary<string, FieldValue>> Data { get; set; }
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}