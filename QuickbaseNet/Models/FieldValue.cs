using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    public class FieldValue
    {
        [JsonProperty("value")]
        public dynamic Value { get; set; }
    }
}