using Newtonsoft.Json;

namespace QuickbaseNet.Responses
{
    /// <summary>
    /// Represents an error response from the QuickBase API.
    /// </summary>
    public class QuickbaseErrorResponse
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}