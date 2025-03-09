using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Constants;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.QueryApplication.Responses;

namespace ZeroStoreApp.QueryService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : ApiController
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetOrderdQuery() { Id = id };
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result, ResponseMessages.Orders.OrderRetrieved, result.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] GetPaginatedOrderQuery request, CancellationToken cancelationToken)
    {
        var result = await mediator.Send(request, cancelationToken);

        return Ok(result);
    }
}
