﻿using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Infra;

public class ZeroStoreAppDbContext : DbContext
{
    public ZeroStoreAppDbContext() : base()
    {
    }

    public ZeroStoreAppDbContext(DbContextOptions<ZeroStoreAppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; private set; }
    public virtual DbSet<Order> Orders { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZeroStoreAppDbContext).Assembly);
    }
}
