using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Requests;

public class QuickbaseQueryRequest
{
    [JsonProperty("from")]
    public string From { get; set; }
    [JsonProperty("select")]
    public List<int> Select { get; set; }
    [JsonProperty("where")]
    public string Where { get; set; }
    [JsonProperty("sortBy")]
    public List<SortByItem> SortBy { get; set; }
    [JsonProperty("groupBy")]
    public List<GroupByItem> GroupBy { get; set; }
    [JsonProperty("options")]
    public Options Options { get; set; }
}