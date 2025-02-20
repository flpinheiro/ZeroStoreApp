using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandApplication.Events;

public class CreateProductEvent : ProductResponse, INotification { }
public class UpdateProductEvent : ProductResponse, INotification { }
public class DeleteProductEvent : ProductResponse, INotification { }
