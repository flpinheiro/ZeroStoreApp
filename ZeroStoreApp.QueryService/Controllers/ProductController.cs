using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroStoreApp.CrossCutting.Constants;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.QueryApplication.Validaators;
using ZeroStoreApp.QueryService.Responses;

namespace ZeroStoreApp.QueryService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetPaginatedProductsQuery query, CancellationToken cancellationToken)
    {
        var validator = new GetPaginatedProductsQueryValidator();
        var validatorResult = await validator.ValidateAsync(query, cancellationToken);

        if(!validatorResult.IsValid) return BadRequest(validatorResult.Errors);

        var products = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResponse<PaginatedResponse<PaginatedProductResponse>>(products, ResponseMessages.Products.PaginatedProductsRetrieved, products.Data.Count()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken) 
    {
        var query = new GetProductQuery { Id = id };
        var validator = new GetProductQueryValidator();

        var validatorResult = await validator.ValidateAsync(query, cancellationToken);

        if(!validatorResult.IsValid) return BadRequest(validatorResult.Errors);

        var product = await _mediator.Send(query, cancellationToken);

        if(product is null) return NotFound(new ApiResponse(ResponseMessages.Products.ProductNotFound, id));

        return Ok(new ApiResponse<ProductResponse>(product, ResponseMessages.Products.ProductRetrieved, id));
    }
}
