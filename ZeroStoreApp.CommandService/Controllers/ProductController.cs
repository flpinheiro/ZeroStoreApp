using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Validators;

using ZeroStoreApp.CrossCutting.Constants;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandService.Controllers;

/// <summary>
/// Controller for handling product related operations of Command Service (post, put, delete, patch)
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : BaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor for ProductController
    /// </summary>
    /// <param name="mediator"></param>
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="command"><see cref="CreateProductCommand"/> create a product command</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ProductResponse"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
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
        var response = await _mediator.Send(command, cancellationToken);

        if(response == null)
        {
            return NotFound();
        }
        
        
        return Ok(response, ResponseMessages.Products.ProductCreated, response.Id);
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="command"><see cref="UpdateProductCommand"/> Update a Product Command</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ProductResponse"/></returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
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
        var response = await _mediator.Send(command, cancellationToken);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response, ResponseMessages.Products.ProductUpdated, response.Id);
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">product id to be deleted</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ProductResponse"/></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
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
        var response = await _mediator.Send(command, cancellationToken);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response, ResponseMessages.Products.ProductDeleted, response.Id);
    }
}
