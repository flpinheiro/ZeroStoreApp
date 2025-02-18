using MediatR;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.QueryApplication.Queries;

public class GetProductQuery : IRequest<ProductResponse>
{
    public Guid Id { get; set; }
}

public class GetPaginatedProductsQuery : GetPaginatedQuery, IRequest<PaginatedResponse<PaginatedProductResponse>>
{
}