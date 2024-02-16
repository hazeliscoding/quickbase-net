using System.Collections.Generic;
using System.Linq;
using QuickbaseNet.Models;
using QuickbaseNet.Requests;

namespace QuickbaseNet.Helpers
{
    /// <summary>
    /// Helper class for building QuickBase API queries.
    /// </summary>
    public class QuickbaseQueryBuilder
    {
        private string _from;
        private List<int> _select;
        private string _where;
        private List<SortByItem> _sortBy;
        private List<GroupByItem> _groupBy;

        /// <summary>
        /// Specifies the table to query.
        /// </summary>
        /// <param name="from">The ID or name of the QuickBase table.</param>
        /// <returns>The current instance of QuickbaseQueryBuilder.</returns>
        public QuickbaseQueryBuilder From(string from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// Specifies which fields to select in the query result.
        /// </summary>
        /// <param name="fields">The IDs of the fields to select.</param>
        /// <returns>The current instance of QuickbaseQueryBuilder.</returns>
        public QuickbaseQueryBuilder Select(params int[] fields)
        {
            _select = fields.ToList();
            return this;
        }

        /// <summary>
        /// Specifies the WHERE clause of the query.
        /// </summary>
        /// <param name="where">The WHERE clause.</param>
        /// <returns>The current instance of QuickbaseQueryBuilder.</returns>
        public QuickbaseQueryBuilder Where(string where)
        {
            _where = where;
            return this;
        }

        /// <summary>
        /// Specifies the fields to sort the query result by.
        /// </summary>
        /// <param name="fieldId">The ID of the field to sort by.</param>
        /// <param name="order">The sorting order ("ASC" for ascending, "DESC" for descending).</param>
        /// <returns>The current instance of QuickbaseQueryBuilder.</returns>
        public QuickbaseQueryBuilder SortBy(int fieldId, string order)
        {
            if (_sortBy == null)
                _sortBy = new List<SortByItem>();

            _sortBy.Add(new SortByItem { FieldId = fieldId, Order = order });
            return this;
        }

        /// <summary>
        /// Specifies the fields to group the query result by.
        /// </summary>
        /// <param name="fieldId">The ID of the field to group by.</param>
        /// <param name="grouping">The grouping criteria.</param>
        /// <returns>The current instance of QuickbaseQueryBuilder.</returns>
        public QuickbaseQueryBuilder GroupBy(int fieldId, string grouping)
        {
            if (_groupBy == null)
                _groupBy = new List<GroupByItem>();

            _groupBy.Add(new GroupByItem { FieldId = fieldId, Grouping = grouping });
            return this;
        }

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <returns>A QuickbaseQueryRequest object representing the query.</returns>
        public QuickbaseQueryRequest Build()
        {
            return new QuickbaseQueryRequest
            {
                From = _from,
                Select = _select,
                Where = _where,
                SortBy = _sortBy,
                GroupBy = _groupBy
            };
        }
    }
}
