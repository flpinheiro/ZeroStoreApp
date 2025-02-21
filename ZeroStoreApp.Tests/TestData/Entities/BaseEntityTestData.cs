using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Tests.TestData.Entities;

internal abstract class BaseEntityTestData<TClass> where TClass : BaseEntity
{
    protected Faker<TClass> faker =
        new Faker<TClass>(Locale)
        .StrictMode(true)
        .RuleFor(p => p.Id, f => f.Random.Uuid())
        .RuleFor(p => p.CreatedAt, f => f.Date.Past())
        .RuleFor(p => p.IsDeleted, f => f.Random.Bool())
        .RuleFor(p => p.UpdatedAt, f => null)
        .RuleFor(p => p.DeletedAt, (f, c) => c.IsDeleted ? f.Date.Past() : null);

    public BaseEntityTestData<TClass> WithUpdate(DateTime updatedAt)
    {
        faker.RuleFor(p => p.UpdatedAt, f => updatedAt);
        return this;
    }

    public BaseEntityTestData<TClass> WithDelete(bool isDelete, DateTime? deleteAt = null)
    {
        faker
            .RuleFor(p => p.IsDeleted, isDelete)
            .RuleFor(p => p.DeletedAt, (f, c) => (isDelete && deleteAt == null) ? f.Date.Past(refDate: c.CreatedAt) : deleteAt);
        return this;
    }

    public virtual TClass Build() { return faker; }

    public virtual IEnumerable<TClass> Build(int count) { return faker.Generate(count); }

    public virtual PaginatedList<TClass> Build(int count, int page, int pageSize)
    {
        var entities = Build(count);

        var pagedList = new PaginatedList<TClass>(entities, entities.Count(), page, pageSize);

        return pagedList;
    }
}
