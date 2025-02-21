using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Infra.Services;

namespace ZeroStoreApp.Tests.Fixtures;

public class UnitOfQueryFixture
{
    private readonly IUnitOfQuery _unitOfQuery;
    public IUnitOfQuery UnitOfQuery { get => _unitOfQuery; }

    private readonly Lazy<IQueryProductRepRepository> lazyProductRepository;
    private readonly Mock<IQueryProductRepRepository> _productRepository;

    public Mock<IQueryProductRepRepository> ProductRepository { get => _productRepository; }

    public UnitOfQueryFixture()
    {
        _productRepository = new Mock<IQueryProductRepRepository>(MockBehavior.Strict);

        lazyProductRepository = new Lazy<IQueryProductRepRepository>(() => _productRepository.Object);

        _unitOfQuery = new UnitOfQuery(lazyProductRepository);
    }

    public void VerifyAll() => _productRepository.VerifyAll();

    public void Reset() => _productRepository.Reset();

    public void VerifyNoOtherCalls() => _productRepository.VerifyNoOtherCalls();

}
