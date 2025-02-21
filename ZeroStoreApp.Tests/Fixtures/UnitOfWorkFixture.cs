using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Infra;
using ZeroStoreApp.Infra.Services;

namespace ZeroStoreApp.Tests.Fixtures;

public class UnitOfWorkFixture
{
    private readonly IUnitOfWork _unitOfWork;
    public IUnitOfWork UnitOfWork { get => _unitOfWork; }

    private readonly Lazy<IProductRepository> lazyProductRepository;
    private readonly Mock<IProductRepository> _productRepository;
    public Mock<IProductRepository> ProductRepository { get => _productRepository; }

    private readonly Mock<ZeroStoreAppDbContext> _context;
    public Mock<ZeroStoreAppDbContext> DbContext { get => _context; }

    public UnitOfWorkFixture()
    {
        _productRepository = new Mock<IProductRepository>(MockBehavior.Strict);
        _context = new Mock<ZeroStoreAppDbContext>(MockBehavior.Strict);

        lazyProductRepository = new Lazy<IProductRepository>(() => _productRepository.Object);

        _unitOfWork = new UnitOfWork(_context.Object, lazyProductRepository);
    }

    public void VerifyAll() => _productRepository.VerifyAll();

    public void Reset() => _productRepository.Reset();

    public void VerifyNoOtherCalls() => _productRepository.VerifyNoOtherCalls();

}
