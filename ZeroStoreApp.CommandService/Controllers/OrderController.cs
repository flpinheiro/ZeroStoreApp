using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Validators;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Constants;

namespace ZeroStoreApp.CommandService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController: ApiController 
{ 
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors, ResponseMessages.ValidationFailed);
        }

        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
}
