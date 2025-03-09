using ZeroStoreApp.CrossCutting.Enums;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Tests.TestData.Entities;

internal class OrderTestData : BaseEntityTestData<Order>
{
    public OrderTestData()
    {
        faker.RuleFor(o => o.TotalValue, 0)
            .RuleFor(o => o.Status, OrderStatus.Created);
    }
}
