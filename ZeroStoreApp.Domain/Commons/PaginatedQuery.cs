namespace ZeroStoreApp.Domain.Commons;

public record PaginatedQuery
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string? Query { get; init; }
}
