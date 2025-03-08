using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Dtos;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Tests.TestData.Dtos;

namespace ZeroStoreApp.Tests.TestData.Commands;

internal class CreateOrderCommandTestData : BasicTestData<CreateOrderCommand>
{
    public CreateOrderCommandTestData()
    {
        faker
            .RuleFor(o => o.Items, f => new OrderItemDtoTestData().Build(f.Random.Number(1, 30)));
    }

    public CreateOrderCommandTestData WithProducts(IEnumerable<Product> products)
    {
        faker.RuleFor<IEnumerable<OrderItemDto>>(o => o.Items, f =>
        {
            var list = products.Take<Product>(f.Random.Number(1, products.Count())).ToList();

            var items = new List<OrderItemDto>();

            foreach (var item in list)
            {
                items.Add(new OrderItemDto { ProductId = item.Id, Quantity = f.Random.Number(1, 30) });
            }
            return items;
        });

        return this;
    }

}
