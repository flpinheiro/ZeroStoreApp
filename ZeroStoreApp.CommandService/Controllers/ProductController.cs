using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandService.Responses;
using ZeroStoreApp.CrossCutting.Constants;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponseWithData<ProductResponse>(response, ResponseMessages.ProductCreated));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponseWithData<ProductResponse>(response, ResponseMessages.ProductUpdated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand { Id = id };
        var response = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponseWithData<ProductResponse>(response, ResponseMessages.ProductDeleted));
    }
}
