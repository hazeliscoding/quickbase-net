using QuickbaseNet.Models;
using QuickbaseNet.Requests;

namespace QuickbaseNet.Helpers;

public class QuickbaseQueryBuilder
{
    private string _from;
    private List<int> _select;
    private string _where;
    private List<SortByItem> _sortBy;
    private List<GroupByItem> _groupBy;

    public QuickbaseQueryBuilder From(string from)
    {
        _from = from;
        return this;
    }

    public QuickbaseQueryBuilder Select(params int[] fields)
    {
        _select = fields.ToList();
        return this;
    }

    public QuickbaseQueryBuilder Where(string where)
    {
        _where = where;
        return this;
    }

    public QuickbaseQueryBuilder SortBy(int fieldId, string order)
    {
        if (_sortBy == null)
            _sortBy = new List<SortByItem>();

        _sortBy.Add(new SortByItem { FieldId = fieldId, Order = order });
        return this;
    }

    public QuickbaseQueryBuilder GroupBy(int fieldId, string grouping)
    {
        if (_groupBy == null)
            _groupBy = new List<GroupByItem>();

        _groupBy.Add(new GroupByItem { FieldId = fieldId, Grouping = grouping });
        return this;
    }

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