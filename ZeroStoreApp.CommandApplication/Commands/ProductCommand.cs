using MediatR;

namespace ZeroStoreApp.CommandApplication.Commands;

public class CreateProductCommand : IRequest<Guid?>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class UpdateProductCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class DeleteProductCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
}

