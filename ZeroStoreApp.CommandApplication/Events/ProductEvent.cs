using MediatR;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandApplication.Events;

public class CreateProductEvent : ProductResponse, INotification { }
public class UpdateProductEvent : ProductResponse, INotification { }
public class DeleteProductEvent : ProductResponse, INotification { }
