using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZeroStoreApp.Domain.Responses;

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

public class PaginatedProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
