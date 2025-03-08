using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStoreApp.CommandApplication.Dtos;

public record OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
