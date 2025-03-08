using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CommandApplication.Dtos;

namespace ZeroStoreApp.CommandApplication.Commands;

public class CreateOrderCommand : IRequest
{
    public IEnumerable<OrderItemDto> Items { get; set; } = [];
}
