using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace QuickbaseNet.Models;

public class Metadata
{
    [JsonProperty("totalRecords")]
    public int TotalRecords { get; set; }
    [JsonProperty("numRecords")]
    public int NumRecords { get; set; }
    [JsonProperty("numFields")]
    public int NumFields { get; set; }
    [JsonProperty("skip")]
    public int Skip { get; set; }
}