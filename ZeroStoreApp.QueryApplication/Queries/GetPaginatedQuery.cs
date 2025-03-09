namespace ZeroStoreApp.QueryApplication.Queries;

public abstract class GetPaginatedQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
