using System.Text;

namespace Agora.Common.Contracts;

public class PagedRequest
{
    private const int DefaultPageSize = 20;

    internal PagedRequest(int page, int pageSize, List<SortDefinition>? sorts = null)
    {
        Page = page;
        PageSize = pageSize;
        Sorts = new SortDefinitions(sorts ?? []);
    }

    public PagedRequest() : this(1, DefaultPageSize, null)
    {
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public SortDefinitions Sorts { get; set; }

    public static IPagedRequestBuilder Create(int page = 1, int pageSize = DefaultPageSize) => new PagedRequestBuilder(page, pageSize);

    public virtual string ToQueryString()
    {
        return new StringBuilder()
            .Append($"?page={Page}&pageSize={PageSize}")
            .Append($"{Sorts.ToQueryString()}")
            .ToString();
    }
}

public class PagedRequest<T> : PagedRequest where T : IRequestFilter
{

    internal PagedRequest(int page, int pageSize, List<SortDefinition> sorts, IRequestFilter filter) : base(page, pageSize, sorts) 
    {
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
    }

    public IRequestFilter Filter { get; set; }

    public override string ToQueryString()
    {
        return new StringBuilder(base.ToQueryString())
            .Append($"{Filter.ToQueryString()}")
            .ToString();
    }
}
