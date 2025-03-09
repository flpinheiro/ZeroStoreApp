using MediatR;

namespace ZeroStoreApp.CommandApplication.Events;

public class CreateOrderEvent : INotification
{
    public Guid OrderId { get; set; }
}
