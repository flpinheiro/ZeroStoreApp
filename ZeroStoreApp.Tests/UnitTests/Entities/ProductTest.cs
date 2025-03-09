using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Tests.TestData.Entities;
using ZeroStoreApp.Tests.TestData.ValueObjects;

namespace ZeroStoreApp.Tests.UnitTests.Entities;

public class ProductTest
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(4, 5)]
    [InlineData(10, 5)]
    [InlineData(11, 10)]
    [InlineData(20, 10)]
    [InlineData(30, 15)]
    public void Should_Calculate_discount(int quantity, decimal expectedDiscount)
    {
        var product = new Product();

        var discount = product.CalculateDiscount(quantity);

        Assert.Equal(expectedDiscount, discount);
    }
}

public class OrderTest
{
    [Fact]
    public void Should_Calculate_Total_Value()
    {
        var order = new OrderTestData().Build();

        var products = new ProductTestData().Build(10);

        var orderItems = OrderItemTestData.Build(products, order);

        var orderItem = new OrderItemTestData().Build();

        order.AddItem(orderItems);

        Assert.NotEqual(0, order.TotalValue);

        Assert.Equal(10, order.Items.Count);

        order.AddItem(orderItem);

        Assert.Equal(11, order.Items.Count);

        order.RemoveItem(orderItem);

        Assert.Equal(10, order.Items.Count);

        order.RemoveItem(orderItems);

        Assert.Empty(order.Items);

        Assert.Equal(0, order.TotalValue);
    }
}
