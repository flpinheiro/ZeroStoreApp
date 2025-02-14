using MediatR;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandApplication.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class UpdateProductCommand : IRequest<ProductResponse>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class DeleteProductCommand : IRequest<ProductResponse>
{
    public Guid Id { get; set; }
}

