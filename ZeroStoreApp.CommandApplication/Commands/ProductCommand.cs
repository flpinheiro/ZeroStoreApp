using MediatR;
using ZeroStoreApp.CommandApplication.Responses;

namespace ZeroStoreApp.CommandApplication.Commands;

public class CreateProductCommand : IRequest<bool>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class GetProductCommand : IRequest<ProductResponse>
{
    public Guid Id { get; set; }
}

public class GetProductsCommand : GetPaginatedCommand, IRequest<PaginatedResponse<PaginatedProductResponse>>
{
}
