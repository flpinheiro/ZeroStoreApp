﻿using ZeroStoreApp.Domain.Repositories;

namespace ZeroStoreApp.Domain.Services;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);

    Task BegingTransactionAsync(CancellationToken cancellationToken);

    Task RollbackTransactionAsync(CancellationToken cancellationToken);

    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
}

public interface IUnitOfQuery : IDisposable
{
    IQueryProductRepRepository Products { get; }
    IQueryOrderRepository Orders { get; }
}
