using ZeroStoreApp.CrossCutting.Enums;

namespace ZeroStoreApp.QueryApplication.Responses;

public record OrderResponse
{
    public Guid Id { get; set; }
    public decimal TotalValue { get; set; }
    public OrderStatus Status { get; set; }

    public IEnumerable<OrderItemResponse> Items { get; set; } = [];
}

public record PaginatedOrderResponse
{
    public Guid Id { get; set; }
    public decimal TotalValue { get; set; }
    public OrderStatus Status { get; set; }
}

public record OrderItemResponse
{
    public Guid ProductId { get; set; }

    public decimal UnitValue { get; set; }

    public string? Name { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalValue { get; set; }


}
