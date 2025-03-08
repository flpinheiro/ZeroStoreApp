using MediatR;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.CommandApplication.Handlers;
using ZeroStoreApp.CommandApplication.Profiles;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Tests.Fixtures;
using ZeroStoreApp.Tests.TestData.Commands;
using ZeroStoreApp.Tests.TestData.Entities;

namespace ZeroStoreApp.Tests.UnitTests.Handlers;

public class OrderCommandHandlerTest : IDisposable
{
    private readonly OrderCommandHandler _handler;

    private readonly Mock<IPublisher> _publisher;
    private readonly UnitOfWorkFixture _unitOfWorkFixture;

    public OrderCommandHandlerTest()
    {
        _unitOfWorkFixture = new UnitOfWorkFixture();

        _publisher = new Mock<IPublisher>(MockBehavior.Strict);

        var mapper = IMapperFixture.GetMapper(new ProductCommandProfile());

        _handler = new OrderCommandHandler(_unitOfWorkFixture.UnitOfWork, _publisher.Object);
    }

    [Fact]
    public async Task Should_Create_Order()
    {
        var products = new ProductTestData().Build(100);
        var command = new CreateOrderCommandTestData().WithProducts(products).Build();

        _unitOfWorkFixture.ProductRepository
            .Setup(x => x.GetManyByIdAsync(It.IsAny<IEnumerable<Guid>>(), default))
            .ReturnsAsync(products);

        _unitOfWorkFixture.OrderRepository.Setup(x => x.AddAsync(It.IsAny<Order>(), default)).Returns(Task.CompletedTask);

        _publisher.Setup(x => x.Publish(It.IsAny<CreateOrderEvent>(), default)).Returns(Task.CompletedTask);

        await _handler.Handle(command, default);
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