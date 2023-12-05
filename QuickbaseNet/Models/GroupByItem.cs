using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace QuickbaseNet.Models;

public class GroupByItem
{
    [JsonProperty("fieldId")]
    public int FieldId { get; set; }
    [JsonProperty("grouping")]
    public string Grouping { get; set; }
}