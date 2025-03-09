using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Validators;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Constants;

namespace ZeroStoreApp.CommandService.Controllers;

/// <summary>
/// Controller for handling product related operations of Command Service (post, put, delete, patch)
/// </summary>
[Route("api/[controller]")]
[ApiController]
public sealed class ProductController(IMediator mediator) : ApiController
{
    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="command"><see cref="CreateProductCommand"/> create a product command</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors, ResponseMessages.ValidationFailed);
        }
        var response = await mediator.Send(command, cancellationToken);

        if (response == null)
        {
            return NotFound();
        }
        return Ok(response, ResponseMessages.Products.ProductCreated, response);
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="command"><see cref="UpdateProductCommand"/> Update a Product Command</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Guid"/></returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors, ResponseMessages.ValidationFailed);
        }
        var response = await mediator.Send(command, cancellationToken);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response, ResponseMessages.Products.ProductUpdated, response);
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">product id to be deleted</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Guid"/></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand { Id = id };

        var validator = new DeleteProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors, ResponseMessages.ValidationFailed);
        }
        var response = await mediator.Send(command, cancellationToken);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response, ResponseMessages.Products.ProductDeleted, response);
    }
}
