using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.ValueObjects;

namespace ZeroStoreApp.Tests.UnitTests.ValueObjects;

public class OrderItemTest
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 10)]
    [InlineData(4, 38)]
    [InlineData(10, 95)]
    [InlineData(15, 135)]
    [InlineData(20, 180)]
    [InlineData(30, 255)]
    public void Should_Calculate_Total_Value(int quantity, decimal expectedTotalValue)
    {
        var product = new Product()
        {
            Price = 10,
        };
        var order = new Order();
        var orderItem = new OrderItem(product, order, quantity);

        Assert.Equal(expectedTotalValue, orderItem.TotalValue);
    }

}
