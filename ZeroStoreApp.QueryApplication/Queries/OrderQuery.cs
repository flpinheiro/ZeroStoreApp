using MediatR;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.QueryApplication.Responses;

namespace ZeroStoreApp.QueryApplication.Queries;

public class GetOrderdQuery : IRequest<OrderResponse>
{
    public Guid Id { get; set; }
}

public class GetPaginatedOrderQuery : GetPaginatedQuery, IRequest<PaginatedList<PaginatedOrderResponse>> { }