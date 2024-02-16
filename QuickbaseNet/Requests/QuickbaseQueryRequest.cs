using System.Collections.Generic;
using Newtonsoft.Json;
using QuickbaseNet.Models;

namespace QuickbaseNet.Requests
{
    /// <summary>
    /// Represents a request to perform a query in the QuickBase API.
    /// </summary>
    public class QuickbaseQueryRequest
    {
        /// <summary>
        /// Gets or sets the ID or name of the table from which to query records.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the list of field IDs to select in the query.
        /// </summary>
        [JsonProperty("select")]
        public List<int> Select { get; set; }

        /// <summary>
        /// Gets or sets the WHERE clause specifying conditions for the query.
        /// </summary>
        [JsonProperty("where")]
        public string Where { get; set; }

        /// <summary>
        /// Gets or sets the list of fields and sort order to sort the query results by.
        /// </summary>
        [JsonProperty("sortBy")]
        public List<SortByItem> SortBy { get; set; }

        /// <summary>
        /// Gets or sets the list of fields and grouping criteria to group the query results by.
        /// </summary>
        [JsonProperty("groupBy")]
        public List<GroupByItem> GroupBy { get; set; }

        /// <summary>
        /// Gets or sets the options for controlling various aspects of the query.
        /// </summary>
        [JsonProperty("options")]
        public Options Options { get; set; }
    }
}