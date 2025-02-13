namespace ZeroStoreApp.CommandApplication.Commands;

public abstract class  GetPaginatedCommand
{
    public string? Query { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
