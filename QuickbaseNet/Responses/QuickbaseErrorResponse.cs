using Newtonsoft.Json;

namespace QuickbaseNet.Responses;

public class QuickbaseErrorResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
}