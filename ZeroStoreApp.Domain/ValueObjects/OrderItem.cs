using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Domain.ValueObjects;

public record OrderItem : ValueRecord
{
    private decimal discount;
    private int quantity;

    public OrderItem(Product product, Order order, int quantity)
    {
        ProductId = product.Id;
        UnitValue = product.Price;
        Product = product;
        Order = order;
        OrderId = order.Id;
        Name = product.Name;
        Discount = product.CalculateDiscount(quantity);
        Quantity = quantity;
        CalculateTotalValue();
    }

    public OrderItem(Guid productId, Guid orderId, decimal unitValue, int quantity, decimal discount, decimal totalValue, string name)
    {
        ProductId = productId;
        OrderId = orderId;
        UnitValue = unitValue;
        Quantity = quantity;
        Discount = discount;
        TotalValue = totalValue;
        Name = name;
    }

    public Guid ProductId { get; private set; }
    public Product? Product { get; set; }

    public Guid OrderId { get; private set; }
    public Order? Order { get; set; }

    public decimal UnitValue { get; private set; }

    public string Name { get; private set; }
    public int Quantity
    {
        get => quantity;
        private set
        {
            if (quantity < 0) throw new Exception();
            quantity = value;
        }
    }
    public decimal Discount
    {
        get => discount;
        private set
        {
            if (discount < 0 || discount > 100) throw new Exception();
            discount = value;
        }
    }
    public decimal TotalValue { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return OrderId;
    }

    private void CalculateTotalValue()
    {
        TotalValue = UnitValue * Quantity * (1 - Discount / 100M);
    }
}
