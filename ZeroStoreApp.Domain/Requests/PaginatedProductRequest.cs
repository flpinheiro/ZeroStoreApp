using ZeroStoreApp.CrossCutting.Common;

namespace ZeroStoreApp.Domain.Requests;

public record PaginatedProductRequest : PaginatedRequest
{
    public string? Query { get; set; }
}

public record PaginateOrderRequest: PaginatedRequest { }
