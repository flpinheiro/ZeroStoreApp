using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.ValueObjects;

namespace ZeroStoreapp.MigrationService.SeedData;

internal static class ProductSeedData
{
    public static IEnumerable<Product> GetProducts()
    {
        return new Faker<Product>("pt_BR")
            .StrictMode(true)
            .RuleFor(p => p.Id, f => f.Random.Uuid())
            .RuleFor(p => p.CreatedAt, f => f.Date.Past())
            .RuleFor(p => p.IsDeleted, f => f.Random.Bool())
            .RuleFor(p => p.UpdatedAt, f => f.Random.Bool() ? null: f.Date.Recent())
            .RuleFor(p => p.DeletedAt, (f, c) => c.IsDeleted ? f.Date.Recent() : null)
            .RuleFor(p => p.Name, f => f.Commerce.Product())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100))
            .RuleFor(p => p.Stock, f => f.Random.Int(1, 1000))
            .Generate(100);
    }
}

internal static class OrderItemSeedData 
{
    public static IEnumerable<OrderItem> GetOrderItems(IEnumerable<Product> products, Order order)
    {
        var faker = new Faker();
        var productList = products
            .Where(x => x.IsDeleted != faker.Random.Bool())
            .Skip(faker.Random.Number(1,20))
            .Take(faker.Random.Number(1, 20))
            .ToList();

        var list = new List<OrderItem>(productList.Count());
        foreach (var product in productList)
        {
            var orderItem = new OrderItem(product, order, faker.Random.Number(1, 30));
            list.Add(orderItem);
        }
        return list;
    }
}
