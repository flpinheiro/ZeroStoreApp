using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Infra;
using ZeroStoreApp.Infra.Services;

namespace ZeroStoreApp.Tests.Fixtures;

public class UnitOfWorkFixture
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly Lazy<IProductRepository> lazyProductRepository;
    private readonly Mock<IProductRepository> _productRepository;

    private readonly Lazy<IOrderRepository> lazyOrderRepository;
    private readonly Mock<IOrderRepository> _orderRepository;

    private readonly Mock<ZeroStoreAppDbContext> _context;
    
    public IUnitOfWork UnitOfWork { get => _unitOfWork; }
    public Mock<ZeroStoreAppDbContext> DbContext { get => _context; }
    public Mock<IProductRepository> ProductRepository { get => _productRepository; }
    public Mock<IOrderRepository> OrderRepository { get => _orderRepository; }

    public UnitOfWorkFixture()
    {
        _productRepository = new Mock<IProductRepository>(MockBehavior.Strict);
        _orderRepository = new Mock<IOrderRepository>(MockBehavior.Strict);

        _context = new Mock<ZeroStoreAppDbContext>(MockBehavior.Strict);

        lazyProductRepository = new Lazy<IProductRepository>(() => _productRepository.Object);
        lazyOrderRepository = new Lazy<IOrderRepository>(() => _orderRepository.Object);

        _unitOfWork = new UnitOfWork(_context.Object, lazyProductRepository, lazyOrderRepository);
    }

    public void VerifyAll() => _productRepository.VerifyAll();

    public void Reset() => _productRepository.Reset();

    public void VerifyNoOtherCalls() => _productRepository.VerifyNoOtherCalls();

}
