using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    /// <summary>
    /// Represents a field used for sorting in a QuickBase query.
    /// </summary>
    public class SortByItem
    {
        /// <summary>
        /// Gets or sets the ID of the field used for sorting.
        /// </summary>
        [JsonProperty("fieldId")]
        public int FieldId { get; set; }

        /// <summary>
        /// Gets or sets the sorting order ("ASC" for ascending, "DESC" for descending).
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }
    }
}