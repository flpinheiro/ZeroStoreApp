using ZeroStoreApp.Domain.Commons;

namespace ZeroStoreApp.Domain.Enities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
