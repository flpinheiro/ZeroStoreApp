using MediatR;
using ZeroStoreApp.CommandApplication.Dtos;

namespace ZeroStoreApp.CommandApplication.Commands;

public class CreateOrderCommand : IRequest
{
    public IEnumerable<OrderItemDto> Items { get; set; } = [];
}
