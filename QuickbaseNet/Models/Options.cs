using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    public class Options
    {
        [JsonProperty("skip")]
        public int Skip { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
        [JsonProperty("compareWithAppLocalTime")]
        public bool CompareWithAppLocalTime { get; set; }
    }
}