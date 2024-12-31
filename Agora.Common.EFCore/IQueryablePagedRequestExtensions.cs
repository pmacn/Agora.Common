using Agora.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Agora.Common.EFCore;

public static class IQueryablePagedRequestExtensions
{
    public static async Task<ICollection<T>> TakePageAsync<T>(this IQueryable<T> query, PagedRequest request)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(request);

        return await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
    }
}