namespace Agora.Common.Contracts;

public class PagedResponse<T>
{

    public PagedResponse(int page, int pageSize, int totalCount, List<T> items)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Page must be greater than 0", nameof(page));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));
        }

        if (totalCount < 0)
        {
            throw new ArgumentException("Total count must be greater than or equal to 0", nameof(totalCount));
        }

        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }

    public PagedResponse()
        : this(1, 25, 0, [])
    {
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    public IEnumerable<T> Items { get; set; }
}
