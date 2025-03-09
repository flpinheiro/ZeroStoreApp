using MediatR;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.CommandApplication.Handlers;
using ZeroStoreApp.CommandApplication.Profiles;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Tests.Fixtures;
using ZeroStoreApp.Tests.TestData.Commands;
using ZeroStoreApp.Tests.TestData.Entities;

namespace ZeroStoreApp.Tests.UnitTests.Handlers;

public class ProductCommandHandlerTest : IDisposable
{
    private readonly ProductCommandHandler _handler;

    private readonly Mock<IPublisher> _publisher;
    private readonly UnitOfWorkFixture _unitOfWorkFixture;

    public ProductCommandHandlerTest()
    {
        _unitOfWorkFixture = new UnitOfWorkFixture();

        _publisher = new Mock<IPublisher>(MockBehavior.Strict);

        var mapper = IMapperFixture.GetMapper(new ProductCommandProfile());

        _handler = new ProductCommandHandler(_unitOfWorkFixture.UnitOfWork, _publisher.Object, mapper);
    }

    [Fact]
    public async Task Should_Create_Product()
    {
        var command = new CreateProductCommandTestData().Build();
        var product = new ProductTestData()
            .WithName(command.Name)
            .WithDescription(command.Description)
            .WithPrice(command.Price)
            .WithStock(command.Stock)
            .Build();

        _unitOfWorkFixture.ProductRepository
            .Setup(x =>
                x.AddAsync(It.IsAny<Product>(), default))
            .Returns(Task.CompletedTask);

        _publisher
            .Setup(x =>
                x.Publish(It.IsAny<CreateProductEvent>(), default))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, default);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Throw_OperationCanceledException_On_Create_Product()
    {
        var command = new CreateProductCommandTestData().Build();

        CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(command, cts.Token));
    }

    [Fact]
    public async Task Should_Update_Product()
    {
        var command = new UpdateProductCommandTestData().Build();
        var product = new ProductTestData()
            .WithId(command.Id)
            .WithName(command.Name)
            .WithDescription(command.Description)
            .WithPrice(command.Price)
            .WithStock(command.Stock)
            .Build();

        _unitOfWorkFixture.ProductRepository
            .Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(product);

        _unitOfWorkFixture.ProductRepository
            .Setup(x =>
                x.UpdateAsync(It.IsAny<Product>(), default))
            .Returns(Task.CompletedTask);

        _publisher
            .Setup(x =>
                x.Publish(It.IsAny<UpdateProductEvent>(), default))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, default);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Return_Null_On_Update_Product()
    {
        var command = new UpdateProductCommandTestData().Build();

        _unitOfWorkFixture.ProductRepository
            .Setup(p => p.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(() => null);

        var result = await _handler.Handle(command, default);

        Assert.Null(result);
    }

    [Fact]
    public async Task Should_Throw_OperationCanceledException_On_Update_Product()
    {
        var command = new UpdateProductCommandTestData().Build();

        CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => _handler.Handle(command, cts.Token));
    }

    [Fact]
    public async Task Should_Delete_Product()
    {
        var command = new DeleteProductCommandTestData().Build();
        var product = new ProductTestData()
            .WithId(command.Id)
            .Build();

        _unitOfWorkFixture.ProductRepository
            .Setup(x =>
                x.DeleteAsync(It.IsAny<Guid>(), default))
            .Returns(Task.CompletedTask);

        _unitOfWorkFixture.ProductRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(product);

        _publisher
            .Setup(x =>
                x.Publish(It.IsAny<DeleteProductEvent>(), default))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, default);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_Throw_OperationCanceledException_On_Delete_Product()
    {
        var command = new DeleteProductCommandTestData().Build();

        CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(command, cts.Token));
    }

    public void Dispose()
    {
        _unitOfWorkFixture.VerifyAll();
        _unitOfWorkFixture.VerifyNoOtherCalls();
        _unitOfWorkFixture.Reset();

        _publisher.VerifyAll();
        _publisher.VerifyNoOtherCalls();

        GC.SuppressFinalize(this);
    }
}
