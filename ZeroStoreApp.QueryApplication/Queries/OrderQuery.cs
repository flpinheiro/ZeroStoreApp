﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.QueryApplication.Responses;

namespace ZeroStoreApp.QueryApplication.Queries;

public class GetOrderdQuery : IRequest<OrderResponse>
{
    public Guid Id { get; set; }
}

public class GetPaginatedOrderQuery : GetPaginatedQuery, IRequest<PaginatedList<PaginatedOrderResponse>> { }