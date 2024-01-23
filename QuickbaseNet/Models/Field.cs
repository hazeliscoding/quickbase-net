using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    public class Field
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}