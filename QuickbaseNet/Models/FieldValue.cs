using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace QuickbaseNet.Models;

public class FieldValue
{
    [JsonProperty("value")]
    public dynamic Value { get; set; }
}