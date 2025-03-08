using AutoMapper;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.QueryApplication.Handlers;
using ZeroStoreApp.QueryApplication.Profiles;
using ZeroStoreApp.QueryApplication.Queries;
using ZeroStoreApp.Tests.Fixtures;
using ZeroStoreApp.Tests.TestData.Entities;

namespace ZeroStoreApp.Tests.UnitTests.Handlers;

public class ProductQueryHandlerTest : IDisposable
{
    private readonly ProductQueryHandler _handler;

    private readonly UnitOfQueryFixture _fixture;
    private readonly IMapper _mapper;

    public ProductQueryHandlerTest()
    {
        _fixture = new UnitOfQueryFixture();

        _mapper = IMapperFixture.GetMapper(new ProductQueryProfile());

        _handler = new ProductQueryHandler(_fixture.UnitOfQuery, _mapper);
    }

    [Fact]
    public async Task Should_Get_By_Id()
    {
        var request = new GetProductQuery() { Id = Guid.NewGuid() };

        var product = new ProductTestData()
            .WithId(request.Id)
            .Build();

        _fixture.ProductRepository.Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync(product);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.Equal(request.Id, result.Id);
    }

    [Fact]
    public async Task Should_Throw_OperationCanceledException_On_Get_By_Id()
    {
        var request = new GetProductQuery() { Id = Guid.NewGuid() };

        CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(request, cts.Token));
    }

    [Fact]
    public async Task Should_Get_Paginated()
    {
        var request = new GetPaginatedProductsQuery();

        var products = new ProductTestData().Build(100, request.Page, request.PageSize);

        _fixture.ProductRepository.Setup(p => p.GetPaginatedAsync(It.IsAny<PaginatedProductRequest>(), default)).ReturnsAsync(products);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);

        Assert.Equal(request.Page, result.CurrentPage);
        Assert.Equal(request.PageSize, result.PageSize);
        Assert.Equal(products.Count(), result.Count);
    }

    [Fact]
    public async Task Should_Throw_OperationCanceledException_On_Get_Paginated()
    {
        var request = new GetPaginatedProductsQuery();

        CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => _handler.Handle(request, cts.Token));
    }

    public void Dispose()
    {
        _fixture.VerifyAll();
        _fixture.VerifyNoOtherCalls();
        _fixture.Reset();

        GC.SuppressFinalize(this);
    }
}
