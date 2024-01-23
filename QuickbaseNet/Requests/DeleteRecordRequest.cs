using Newtonsoft.Json;

namespace QuickbaseNet.Requests
{
    public class DeleteRecordRequest
    {
        [JsonProperty("from")]    
        public string From { get; set; }
        [JsonProperty("where")]
        public string Where { get; set; }
    }
}