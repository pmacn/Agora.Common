namespace Agora.Common.Contracts;

public interface IPagedRequestBuilder
{
    PagedRequest Build();
    IPagedRequestBuilder WithSort(List<SortDefinition> sorts);
    IPagedRequestBuilder<T> WithFilter<T>(T filter) where T : IRequestFilter;
}

public interface IPagedRequestBuilder<T> : IPagedRequestBuilder where T : IRequestFilter
{
    new PagedRequest<T> Build();
    new IPagedRequestBuilder<T> WithSort(List<SortDefinition> sorts);
    IPagedRequestBuilder<T> WithFilter(T filter);
}