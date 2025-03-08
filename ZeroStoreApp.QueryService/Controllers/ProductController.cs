using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Constants;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.QueryApplication.Validaators;

namespace ZeroStoreApp.QueryService.Controllers;

/// <summary>
/// Controller for handling product related requests (get)
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : ApiController
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
    /// Get a list of products with pagination
    /// </summary>
    /// <param name="query">query to request a paginated list of products</param>
    /// <param name="cancellationToken"></param>
    /// <returns>list of <see cref="PaginatedProductResponse"/></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<PaginatedList<PaginatedProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts([FromQuery] GetPaginatedProductsQuery query, CancellationToken cancellationToken)
    {
        var validator = new GetPaginatedProductsQueryValidator();
        var validatorResult = await validator.ValidateAsync(query, cancellationToken);

        if(!validatorResult.IsValid) return BadRequest(validatorResult.Errors, ResponseMessages.ValidationFailed);

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response, ResponseMessages.Products.PaginatedProductsRetrieved, response.Count);
    }

    /// <summary>
    /// Get a product by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ProductResponse"/></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken) 
    {
        var query = new GetProductQuery { Id = id };
        var validator = new GetProductQueryValidator();

        var validatorResult = await validator.ValidateAsync(query, cancellationToken);

        if(!validatorResult.IsValid) return BadRequest(validatorResult.Errors, ResponseMessages.ValidationFailed);

        var product = await _mediator.Send(query, cancellationToken);

        if(product is null) return NotFound();

        return Ok(product, ResponseMessages.Products.ProductRetrieved, product.Id);
    }
}
