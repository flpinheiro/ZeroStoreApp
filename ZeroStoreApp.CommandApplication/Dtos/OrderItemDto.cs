namespace ZeroStoreApp.CommandApplication.Dtos;

public record OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
