using ZeroStoreApp.CommandApplication.Commands;

namespace ZeroStoreApp.Tests.TestData.Commands;

internal class CreateProductCommandTestData : BasicTestData<CreateProductCommand>
{
    public CreateProductCommandTestData()
    {
        faker
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000));
    }

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
}

internal class UpdateProductCommandTestData: BasicTestData<UpdateProductCommand>
{
    public UpdateProductCommandTestData()
    {
        faker.RuleFor(p => p.Id, f => f.Random.Uuid())
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
        .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000));
    }   
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
}

internal class DeleteProductCommandTestData : BasicTestData<DeleteProductCommand>
{
    public DeleteProductCommandTestData()
    {
        faker.RuleFor(p => p.Id, f => f.Random.Uuid());
    }
   
    public DeleteProductCommandTestData WithId(Guid id)
    {
        faker.RuleFor(p => p.Id, f => id);
        return this;
    }
}