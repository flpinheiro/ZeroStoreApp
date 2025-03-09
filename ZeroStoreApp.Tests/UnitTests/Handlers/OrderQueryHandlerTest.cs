using AutoMapper;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.QueryApplication.Handlers;
using ZeroStoreApp.QueryApplication.Profiles;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.Tests.Fixtures;
using ZeroStoreApp.Tests.TestData.Entities;

namespace ZeroStoreApp.Tests.UnitTests.Handlers;

public class OrderQueryHandlerTest
{
    private readonly OrderQueryHandler _handler;

    private readonly UnitOfQueryFixture _fixture;
    private readonly IMapper _mapper;
    public OrderQueryHandlerTest()
    {
        _fixture = new UnitOfQueryFixture();
        _mapper = IMapperFixture.GetMapper(new OrderQueryProfile());

        _handler = new OrderQueryHandler(_fixture.UnitOfQuery, _mapper);
    }

    [Fact]
    public async Task Should_Get_Order()
    {
        var query = new GetOrderdQuery()
        {
            Id = Guid.NewGuid(),
        };

        var order = new OrderTestData().Build();

        _fixture.OrderRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync(order);

        var result = await _handler.Handle(query, default);

        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
    }

    [Fact]
    public async Task Should_Get_Paginated_Orders()
    {
        var query = new GetPaginatedOrderQuery();

        var orders = new OrderTestData().Build(10, query.Page, query.PageSize);

        _fixture.OrderRepository.Setup(x => x.GetPaginatedAsync(It.IsAny<PaginateOrderRequest>(), default)).ReturnsAsync(orders);

        var result = await _handler.Handle(query, default);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.Equal(query.Page, result.CurrentPage);
        Assert.Equal(query.PageSize, result.PageSize);
        Assert.Equal(orders.Count(), result.Count);
    }
}
