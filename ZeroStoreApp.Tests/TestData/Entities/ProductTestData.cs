using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Tests.TestData.Entities;

internal class ProductTestData : BaseEntityTestData<Product>
{
    public ProductTestData()
    {
        faker
            .RuleFor(p => p.Name, f => f.Commerce.Product())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
            .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000));
    }

    public ProductTestData WithId(Guid id)
    {
        faker.RuleFor(p => p.Id, f => id);
        return this;
    }

    public ProductTestData WithName(string? name)
    {
        faker.RuleFor(p => p.Name, f => name);
        return this;
    }

    public ProductTestData WithDescription(string? description)
    {
        faker.RuleFor(p => p.Description, f => description);
        return this;
    }

    public ProductTestData WithPrice(decimal price)
    {
        faker.RuleFor(p => p.Price, f => price);
        return this;
    }

    public ProductTestData WithStock(int stock)
    {
        faker.RuleFor(p => p.Stock, f => stock);
        return this;
    }
}
