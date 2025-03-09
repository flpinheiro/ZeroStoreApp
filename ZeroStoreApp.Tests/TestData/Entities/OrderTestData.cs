using ZeroStoreApp.CrossCutting.Enums;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Tests.TestData.ValueObjects;

namespace ZeroStoreApp.Tests.TestData.Entities;

internal class OrderTestData : BaseEntityTestData<Order>
{
    public OrderTestData()
    {
        faker.RuleFor(o => o.TotalValue, 0)
            .RuleFor(o => o.Status, OrderStatus.Created)
            .RuleFor(o => o.Items, faker => new OrderItemTestData().Build(faker.Random.Number(1, 15)));
    }
}
