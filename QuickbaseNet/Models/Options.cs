using Newtonsoft.Json;

namespace QuickbaseNet.Models
{
    /// <summary>
    /// Represents options for controlling various aspects of QuickBase operations.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the number of records to skip in the result set.
        /// </summary>
        [JsonProperty("skip")]
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of records to return in the result set.
        /// </summary>
        [JsonProperty("top")]
        public int Top { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to compare with the application's local time.
        /// </summary>
        [JsonProperty("compareWithAppLocalTime")]
        public bool CompareWithAppLocalTime { get; set; }
    }
}