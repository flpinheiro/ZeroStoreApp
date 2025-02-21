namespace ZeroStoreApp.Domain.Commons;

public abstract record PaginatedRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Query { get; init; }
}
