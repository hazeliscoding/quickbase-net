using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    /// <summary>
    /// Represents a field in a QuickBase table.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Gets or sets the ID of the field.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the label of the field.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}