using System.Collections.ObjectModel;

namespace Agora.Common.Contracts;

public class SortDefinitions : Collection<SortDefinition>
{
    public SortDefinitions()
    {

    }

    public SortDefinitions(IList<SortDefinition> list) : base(list)
    {

    }

    public string ToQueryString()
    {
        var sortValue = string.Join(",", this.Select(x => x.ToQueryStringValue()));
        return !string.IsNullOrWhiteSpace(sortValue) ? $"&sort={sortValue}" : "";
    }
}
