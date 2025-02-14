namespace ZeroStoreApp.QueryApplication.Queries;

public abstract class GetPaginatedQuery
{
    public string? Query { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
