namespace ZeroStoreApp.CommandApplication.Responses;

public record PaginatedResponse<T>(IEnumerable<T> Data, int Page, int PageSize, int Total, int TotalPages);