using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.CrossCutting.Common;

namespace ZeroStoreApp.Infra.Extensions;

public static class PaginatedListExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, PaginatedRequest request, CancellationToken cancellationToken)
    {
        return await source.ToPaginatedListAsync(request.Page, request.PageSize, cancellationToken);
    }
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }

    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, PaginatedRequest request)
    {
        return source.ToPaginatedList(request.Page, request.PageSize);
    }
    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
