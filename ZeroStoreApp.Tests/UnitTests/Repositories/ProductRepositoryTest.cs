using Moq.EntityFrameworkCore;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Infra;
using ZeroStoreApp.Infra.Repositories;
using ZeroStoreApp.Tests.TestData.Entities;

namespace ZeroStoreApp.Tests.UnitTests.Repositories;

public class ProductRepositoryTest
{
    private readonly Mock<ZeroStoreAppDbContext> _context;

    private readonly ProductRepository _repository;

    private List<Product> _products;

    private readonly Product _product;

    public ProductRepositoryTest()
    {
        _context = new Mock<ZeroStoreAppDbContext>(MockBehavior.Strict);

        _repository = new ProductRepository(_context.Object);

        _product = new ProductTestData()
            .WithName("Test product")
            .WithDescription("Description product")
            .WithDelete(false)
            .Build();

        _products = new ProductTestData().Build(100).ToList();

        _products.Add(_product);

        _context.Setup(p => p.Products).ReturnsDbSet(_products);

        _context.Setup(p => p.Set<Product>()).ReturnsDbSet(_products);
    }

    [Fact]
    public async Task Should_Get_Products_By_ids()
    {
        var ids = _products.Take(10).Select(a => a.Id).ToList();

        var products = await _repository.GetManyByIdAsync(ids, default);

        Assert.NotNull(products);

        Assert.Equal(ids.Count, products.Count());
        Assert.True(ids.All(expectedGuid => products.Select(a => a.Id).Contains(expectedGuid)));
    }

    [Fact]
    public async Task Should_Get_product_By_Id()
    {
        foreach (var item in _products)
        {
            var product = await _repository.GetByIdAsync(item.Id, default);

            if (item.IsDeleted)
            {
                Assert.Null(product);
                Assert.NotNull(item.DeletedAt);
            }
            else
            {
                Assert.NotNull(product);
                Assert.Equal(item, product);
            }
        }
        _context.Verify(p => p.Set<Product>());
    }

    [Fact]
    public async Task Should_Get_Paginated()
    {
        var request = new PaginatedProductRequest()
        {
            PageSize = 10,
            Page = 1
        };

        var result = await _repository.GetPaginatedAsync(request, default);

        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.Equal(request.PageSize, result.PageSize);
        Assert.Equal(request.Page, result.CurrentPage);
        Assert.NotEqual(0, result.TotalCount);
        Assert.NotEqual(0, result.TotalPages);

        Assert.True(result.HasNext);
        Assert.False(result.HasPrevious);

        result.ToList().ForEach(item =>
        {
            Assert.False(item.IsDeleted);
            Assert.NotNull(item.Name);
        });

        _context.Verify(p => p.Products);
    }

    [Fact]
    public async Task Should_Get_Query()
    {
        var request = new PaginatedProductRequest
        {
            Query = "test"
        };

        var result = await _repository.GetPaginatedAsync(request, default);

        result.ToList().ForEach(item => Assert.Contains("test", item.Name.ToLowerInvariant()));


        request = new PaginatedProductRequest
        {
            Query = "description"
        };

        result = await _repository.GetPaginatedAsync(request, default);

        result.ToList().ForEach(item => Assert.Contains("description", item.Description.ToLowerInvariant()));

        _context.Verify(p => p.Products);
    }

    [Fact]
    public async Task Should_Add_Async()
    {
        var product = new ProductTestData()
            .WithDelete(false)
            .Build();

        _context.Setup(p => p.SaveChangesAsync(default)).ReturnsAsync(1);

        await _repository.AddAsync(product, default);

        _context.Verify(p => p.SaveChangesAsync(default));
        _context.Verify(p => p.Set<Product>());
    }

    [Fact]
    public async Task Should_Update_Async()
    {
        var product = new ProductTestData()
            .WithDelete(false)
            .Build();

        _context.Setup(p => p.SaveChangesAsync(default)).ReturnsAsync(1);

        await _repository.UpdateAsync(product, default);


        _context.Verify(p => p.SaveChangesAsync(default));
        _context.Verify(p => p.Set<Product>());
    }

    [Fact]
    public async Task Should_Delete_Async()
    {
        _context.Setup(p => p.SaveChangesAsync(default)).ReturnsAsync(1);

        await _repository.DeleteAsync(_product.Id, default);


        _context.Verify(p => p.SaveChangesAsync(default));
        _context.Verify(p => p.Set<Product>());
    }

    [Fact]
    public async Task Should_Return_Null_On_Delete_Async()
    {
        var product = new ProductTestData()
            .WithDelete(false)
            .Build();

        _context.Setup(p => p.SaveChangesAsync(default)).ReturnsAsync(1);

        await _repository.DeleteAsync(product.Id, default);


        _context.Verify(p => p.SaveChangesAsync(default), Times.Never);
        _context.Verify(p => p.Set<Product>());
    }

}
