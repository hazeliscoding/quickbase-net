using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    /// <summary>
    /// Represents a field used for grouping in a QuickBase query.
    /// </summary>
    public class GroupByItem
    {
        /// <summary>
        /// Gets or sets the ID of the field used for grouping.
        /// </summary>
        [JsonProperty("fieldId")]
        public int FieldId { get; set; }

        /// <summary>
        /// Gets or sets the grouping criteria.
        /// </summary>
        [JsonProperty("grouping")]
        public string Grouping { get; set; }
    }
}