using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.ValueObjects;
using static ZeroStoreApp.CrossCutting.Constants.Definitions;

namespace ZeroStoreApp.Infra.Mappings;

internal class OrderConfiguration : BaseEntityConfiguration<Order>, IEntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder); base.Configure(builder);

        builder.Property(x => x.TotalValue)
            .IsRequired();

        builder.OwnsMany(x => x.Items, item =>
        {
            item.WithOwner(x => x.Order)
             .HasForeignKey(x => x.OrderId);

            item.HasKey(x => new { x.OrderId, x.ProductId });

            item.Property(x => x.ProductId)
             .IsRequired()
             .HasColumnName(nameof(OrderItem.ProductId));
            item.Property(x => x.OrderId)
             .IsRequired()
             .HasColumnName(nameof(OrderItem.OrderId));
            item.Property(x => x.UnitValue)
             .IsRequired()
             .HasPrecision(ProductDefinition.PricePrecision, ProductDefinition.PriceScale)
             .HasColumnName(nameof(OrderItem.UnitValue));
            item.Property(x => x.Quantity)
             .IsRequired()
             .HasColumnName(nameof(OrderItem.Quantity));
            item.Property(x => x.Discount)
             .IsRequired()
             .HasPrecision(ProductDefinition.PricePrecision, ProductDefinition.PriceScale)
             .HasColumnName(nameof(OrderItem.Discount));
            item.Property(x => x.TotalValue)
             .IsRequired()
             .HasPrecision(ProductDefinition.PricePrecision, ProductDefinition.PriceScale)
             .HasColumnName(nameof(OrderItem.TotalValue));
            item.Property(x => x.Name)
             .IsRequired()
             .HasColumnName(nameof(OrderItem.Name));

        }).ToTable("OrderItem");

    }
}
