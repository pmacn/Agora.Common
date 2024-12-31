namespace Agora.Common.Contracts;

public class PagedRequestBuilder : IPagedRequestBuilder
{
    protected int _page;
    protected int _pageSize;
    protected List<SortDefinition> _sorts = [];

    internal PagedRequestBuilder(int page, int pageSize)
    {
        _page = page;
        _pageSize = pageSize;
    }

    public IPagedRequestBuilder WithSort(List<SortDefinition> sorts)
    {
        _sorts = sorts;
        return this;
    }

    public IPagedRequestBuilder<T> WithFilter<T>(T filter) where T : IRequestFilter
    {
        return new PagedRequestBuilder<T>(_page, _pageSize, _sorts, filter);
    }

    public PagedRequest Build()
    {
        return new PagedRequest(_page, _pageSize, _sorts);
    }
}

public class PagedRequestBuilder<T> : PagedRequestBuilder, IPagedRequestBuilder<T> where T : IRequestFilter
{
    private T _filter;

    internal PagedRequestBuilder(int page, int pageSize, List<SortDefinition> sorts, T filter) : base(page, pageSize)
    {
        _sorts = sorts;
        _filter = filter;
    }

    public IPagedRequestBuilder<T> WithFilter(T filter)
    {
        _filter = filter;
        return this;
    }

    public new IPagedRequestBuilder<T> WithSort(List<SortDefinition> sorts)
    {
        _sorts = sorts;
        return this;
    }

    public new PagedRequest<T> Build()
    {
        return new PagedRequest<T>(_page, _pageSize, _sorts, _filter);
    }
}
