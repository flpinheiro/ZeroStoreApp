namespace ZeroStoreApp.Domain.Commons;

public abstract record PaginatedQuery
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string? Query { get; init; }
}
