using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Validators;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Constants;

namespace ZeroStoreApp.CommandService.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class OrderController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors, ResponseMessages.ValidationFailed);
        }

        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
