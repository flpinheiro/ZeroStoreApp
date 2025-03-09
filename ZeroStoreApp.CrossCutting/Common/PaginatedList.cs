namespace ZeroStoreApp.CrossCutting.Common;

public class PaginatedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginatedList() { }
    public PaginatedList(IEnumerable<T> items, int count, PaginatedRequest request) : this(items, count, request.Page, request.PageSize) { }
    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize) : this()
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }
}
