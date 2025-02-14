using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Validators;
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
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse(validationResult.Errors, ResponseMessages.ValidationFailed));
        }
        var response = await _mediator.Send(command, cancellationToken);

        if(response == null)
        {
            return NotFound(new ApiResponse(ResponseMessages.Products.ProductNotFound, string.Empty));
        }
        return Ok(new ApiResponse<ProductResponse>(response, ResponseMessages.Products.ProductCreated, response.Id));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse(validationResult.Errors, ResponseMessages.ValidationFailed));
        }
        var response = await _mediator.Send(command, cancellationToken);
        if (response == null)
        {
            return NotFound(new ApiResponse(ResponseMessages.Products.ProductNotFound, string.Empty));
        }
        return Ok(new ApiResponse<ProductResponse>(response, ResponseMessages.Products.ProductUpdated, response.Id));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand { Id = id };

        var validator = new DeleteProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse(validationResult.Errors, ResponseMessages.ValidationFailed));
        }
        var response = await _mediator.Send(command, cancellationToken);
        if (response == null)
        {
            return NotFound(new ApiResponse(ResponseMessages.Products.ProductNotFound, string.Empty));
        }
        return Ok(new ApiResponse<ProductResponse>(response, ResponseMessages.Products.ProductDeleted, response.Id));
    }
}
