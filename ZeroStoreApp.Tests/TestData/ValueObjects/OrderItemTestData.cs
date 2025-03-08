using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.ValueObjects;

namespace ZeroStoreApp.Tests.TestData.ValueObjects;

internal class OrderItemTestData : BasicTestData<OrderItem>
{
    public OrderItemTestData()
    {
        faker.CustomInstantiator(f =>
            {
                var orderItem = new OrderItem(
                    f.Random.Uuid(),
                    f.Random.Uuid(),
                    f.Random.Decimal(1, 100),
                    f.Random.Number(1, 10),
                    f.Random.Decimal(0, 15),
                    f.Random.Decimal(0, 15),
                    f.Commerce.ProductName()
                    );

                return orderItem;
            })
            .RuleFor(o=> o.Order, new Order())
            .RuleFor(o => o.Product, new Product())
            .RuleFor(o => o.ProductId, faker => faker.Random.Uuid())
            .RuleFor(o => o.OrderId, faker => faker.Random.Uuid())
            .RuleFor(o => o.UnitValue, faker => faker.Random.Decimal(1, 30))
            .RuleFor(o => o.Name, faker => faker.Commerce.ProductName())
            .RuleFor(o => o.Quantity, faker => faker.Random.Number(1, 30))
            .RuleFor(o => o.Discount, faker => faker.Random.Decimal(1, 30))
            .RuleFor(o => o.TotalValue, faker => faker.Random.Decimal(1, 30));
    }

    public static IEnumerable<OrderItem> Build(IEnumerable<Product> products)
    {
        var list = new List<OrderItem>(products.Count());
        foreach (var product in products) 
        {
            var orderItem = new OrderItem(product, 10);
            list.Add(orderItem);
        }
        return list;
    }
}
