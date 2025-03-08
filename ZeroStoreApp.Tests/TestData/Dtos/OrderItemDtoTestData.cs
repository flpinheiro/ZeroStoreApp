using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CommandApplication.Dtos;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Tests.TestData.Dtos;

internal class OrderItemDtoTestData : BasicTestData<OrderItemDto>
{
    public OrderItemDtoTestData()
    {
        faker
        .RuleFor(o => o.ProductId, f => f.Random.Guid())
        .RuleFor(o => o.Quantity, f => f.Random.Number(0, 30));
    }

    public OrderItemDtoTestData WithQuantity(int quantity)
    {
        faker.RuleFor(o => o.Quantity, quantity);
        return this;
    }
}
