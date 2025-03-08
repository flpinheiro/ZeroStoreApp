using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStoreApp.CommandApplication.Events;

public class CreateOrderEvent : INotification
{
    public Guid OrderId { get; set; }
}
