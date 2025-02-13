using Bogus;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Tests.TestData.Entities;

internal class ProductTestData
{
    private Faker<Product> faker = 
        new Faker<Product>("pt-br")
        .StrictMode(true)
        .RuleFor(p => p.Id, f => f.Random.Uuid())
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000));

    public ProductTestData WithId(Guid id)
    {
        faker = faker.RuleFor(p => p.Id, f => id);
        return this;
    }

    public ProductTestData WithName(string name)
    {
        faker = faker.RuleFor(p => p.Name, f => name);
        return this;
    }

    public ProductTestData WithDescription(string description)
    {
        faker = faker.RuleFor(p => p.Description, f => description);
        return this;
    }

    public ProductTestData WithPrice(decimal price)
    {
        faker = faker.RuleFor(p => p.Price, f => price);
        return this;
    }

    public ProductTestData WithStock(int stock)
    {
        faker = faker.RuleFor(p => p.Stock, f => stock);
        return this;
    }

    public Product Build() { return faker; }

    public IEnumerable<Product> Build(int count) { return faker.Generate(count); }
}
