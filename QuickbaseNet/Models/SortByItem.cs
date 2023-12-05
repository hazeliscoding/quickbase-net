using Newtonsoft.Json;

namespace QuickbaseNet.Models;

public class SortByItem
{
    [JsonProperty("fieldId")]
    public int FieldId { get; set; }
    [JsonProperty("order")]
    public string Order { get; set; }
}