using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.Domain.Commons;

namespace ZeroStoreApp.Infra.Extensions;

public static class PaginatedResponseExtension
{
    public static PaginatedResult<T> ToPaginated<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {
        var totalItems = query.Count();
        var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedResult<T>(items, totalItems, page, pageSize);
    }

    public static async Task<PaginatedResult<T>> ToPaginatedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {
        var totalItems = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedResult<T>(items, totalItems, page, pageSize);
    }
}
