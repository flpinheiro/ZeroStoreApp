using MediatR;
using System.Text.Json;

namespace ZeroStoreApp.CommandApplication.Events;

public class CreateProductEvent : ProductResponse, INotification { }
public class UpdateProductEvent : ProductResponse, INotification { }
public class DeleteProductEvent : ProductResponse, INotification { }

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public virtual byte[] ToBytes()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this);
    }
}
