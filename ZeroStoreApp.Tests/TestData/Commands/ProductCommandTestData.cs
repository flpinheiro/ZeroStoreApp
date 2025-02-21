using ZeroStoreApp.CommandApplication.Commands;

namespace ZeroStoreApp.Tests.TestData.Commands;

internal class CreateProductCommandTestData
{
    protected Faker<CreateProductCommand> faker = new Faker<CreateProductCommand>(Locale)
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000));

    public CreateProductCommandTestData WithName(string name)
    {
        faker.RuleFor(p => p.Name, f => name);
        return this;
    }

    public CreateProductCommandTestData WithDescription(string description)
    {
        faker.RuleFor(p => p.Description, f => description);
        return this;
    }

    public CreateProductCommandTestData WithPrice(decimal price)
    {
        faker.RuleFor(p => p.Price, f => price);
        return this;
    }

    public CreateProductCommandTestData WithStock(int stock)
    {
        faker.RuleFor(p => p.Stock, f => stock);
        return this;
    }

    public virtual CreateProductCommand Build() { return faker; }

}

internal class UpdateProductCommandTestData
{
    protected Faker<UpdateProductCommand> faker = new Faker<UpdateProductCommand>(Locale)
        .RuleFor(p => p.Id, f => f.Random.Uuid())
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000));
    public UpdateProductCommandTestData WithId(Guid id)
    {
        faker.RuleFor(p => p.Id, f => id);
        return this;
    }
    public UpdateProductCommandTestData WithName(string name)
    {
        faker.RuleFor(p => p.Name, f => name);
        return this;
    }
    public UpdateProductCommandTestData WithDescription(string description)
    {
        faker.RuleFor(p => p.Description, f => description);
        return this;
    }
    public UpdateProductCommandTestData WithPrice(decimal price)
    {
        faker.RuleFor(p => p.Price, f => price);
        return this;
    }
    public UpdateProductCommandTestData WithStock(int stock)
    {
        faker.RuleFor(p => p.Stock, f => stock);
        return this;
    }
    public virtual UpdateProductCommand Build() { return faker; }
}

internal class DeleteProductCommandTestData
{
    protected Faker<DeleteProductCommand> faker = new Faker<DeleteProductCommand>(Locale)
        .RuleFor(p => p.Id, f => f.Random.Uuid());
    public DeleteProductCommandTestData WithId(Guid id)
    {
        faker.RuleFor(p => p.Id, f => id);
        return this;
    }
    public virtual DeleteProductCommand Build() { return faker; }
}