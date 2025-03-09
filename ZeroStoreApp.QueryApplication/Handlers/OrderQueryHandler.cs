using AutoMapper;
using MediatR;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Helpers;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.QueryApplication.Responses;

namespace ZeroStoreApp.QueryApplication.Handlers;

public class OrderQueryHandler(IUnitOfQuery unitOfQuery, IMapper mapper) :
    IRequestHandler<GetOrderdQuery, OrderResponse>,
    IRequestHandler<GetPaginatedOrderQuery, PaginatedList<PaginatedOrderResponse>>
{
    public async Task<OrderResponse> Handle(GetOrderdQuery request, CancellationToken cancellationToken)
    {
        var order = await unitOfQuery.Orders.GetByIdAsync(request.Id, cancellationToken);

        var response = mapper.Map<OrderResponse>(order);

        return response;
    }

    public async Task<PaginatedList<PaginatedOrderResponse>> Handle(GetPaginatedOrderQuery request, CancellationToken cancellationToken)
    {
        var paginatedQuery = mapper.Map<PaginateOrderRequest>(request);

        var result = await unitOfQuery.Orders.GetPaginatedAsync(paginatedQuery, cancellationToken);

        var respsonse = mapper.MapPaginatedList<Order, PaginatedOrderResponse>(result);
        return respsonse;
    }
}
