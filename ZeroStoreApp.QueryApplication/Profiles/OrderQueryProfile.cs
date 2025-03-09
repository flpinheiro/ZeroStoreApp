using AutoMapper;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Domain.ValueObjects;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.QueryApplication.Responses;

namespace ZeroStoreApp.QueryApplication.Profiles;

public class OrderQueryProfile : Profile
{
    public OrderQueryProfile()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<OrderItem, OrderItemResponse>();
        CreateMap<GetPaginatedOrderQuery, PaginateOrderRequest>();
        CreateMap<Order, PaginatedOrderResponse>();
    }
}
