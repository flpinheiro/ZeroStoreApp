using ZeroStoreApp.CrossCutting.Enums;
using ZeroStoreApp.Domain.ValueObjects;

namespace ZeroStoreApp.Domain.Enities;

public class Order : BaseEntity
{
    public decimal TotalValue { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Created;

    public ICollection<OrderItem> Items { get; } = [];

    private void CalculateTotalValue()
    {
        if (Items.Count == 0)
        {
            TotalValue = 0;
            return;
        }
        TotalValue = Items.Sum(Item => Item.TotalValue);
    }
    public void AddItem(IEnumerable<OrderItem> items)
    {
        items.ToList().ForEach(item => Items.Add(item));
        CalculateTotalValue();
    }
    public void AddItem(OrderItem item)
    {
        if (Items.Any(a => a.Equals(item)))
        {
            Items.Remove(item);
        }
        Items.Add(item);
        CalculateTotalValue();
    }

    public void RemoveItem(IEnumerable<OrderItem> items)
    {
        items.ToList().ForEach((item) => Items.Remove(item));
        CalculateTotalValue();
    }
    public void RemoveItem(OrderItem item)
    {
        Items.Remove(item);
        CalculateTotalValue();
    }
}
