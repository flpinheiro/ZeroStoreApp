namespace ZeroStoreApp.CrossCutting.Common;

public abstract record PaginatedRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
