namespace ZeroStoreApp.Domain.Commons;

public sealed record PaginatedResult<T>(IEnumerable<T> Items, int TotalItems, int Page, int PageSize) where T : class
{
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));
}
