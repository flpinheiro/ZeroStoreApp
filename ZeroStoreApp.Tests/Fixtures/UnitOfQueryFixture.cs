using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Infra.Services;

namespace ZeroStoreApp.Tests.Fixtures;

public class UnitOfQueryFixture
{
    private readonly IUnitOfQuery _unitOfQuery;

    private readonly Lazy<IQueryProductRepRepository> lazyProductRepository;
    private readonly Mock<IQueryProductRepRepository> _productRepository;

    private readonly Lazy<IQueryOrderRepository> lazyOrderRepository;
    private readonly Mock<IQueryOrderRepository> _orderRepository;

    public IUnitOfQuery UnitOfQuery { get => _unitOfQuery; }
    public Mock<IQueryProductRepRepository> ProductRepository { get => _productRepository; }
    public Mock<IQueryOrderRepository> OrderRepository { get => _orderRepository; }

    public UnitOfQueryFixture()
    {
        _productRepository = new Mock<IQueryProductRepRepository>(MockBehavior.Strict);
        _orderRepository = new Mock<IQueryOrderRepository>(MockBehavior.Strict);

        lazyProductRepository = new Lazy<IQueryProductRepRepository>(() => _productRepository.Object);
        lazyOrderRepository = new Lazy<IQueryOrderRepository>(() => _orderRepository.Object);

        _unitOfQuery = new UnitOfQuery(lazyProductRepository, lazyOrderRepository);
    }

    public void VerifyAll() => _productRepository.VerifyAll();

    public void Reset() => _productRepository.Reset();

    public void VerifyNoOtherCalls() => _productRepository.VerifyNoOtherCalls();
}
