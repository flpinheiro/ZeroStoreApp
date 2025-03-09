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
        base.Configure(builder);

        builder.Property(x => x.TotalValue)
            .HasPrecision(ProductDefinition.PricePrecision, ProductDefinition.PriceScale)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.OwnsMany(x => x.Items, item =>
        {
            item.WithOwner(x => x.Order)
             .HasForeignKey(x => x.OrderId);

            item.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);

            item.HasKey(x => new { x.OrderId, x.ProductId });

            item.HasIndex(x => new { x.OrderId, x.ProductId })
                .IsUnique();
            item.HasIndex(x => new { x.OrderId });

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
             .HasMaxLength(ProductDefinition.NameMaxLength)
             .HasColumnName(nameof(OrderItem.Name));
        });
    }
}
