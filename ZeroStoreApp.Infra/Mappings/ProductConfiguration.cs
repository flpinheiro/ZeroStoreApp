using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZeroStoreApp.Domain.Enities;
using static ZeroStoreApp.CrossCutting.Constants.Definitions;

namespace ZeroStoreApp.Infra.Mappings;

internal class ProductConfiguration : BaseEntityConfiguration<Product>, IEntityTypeConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(ProductDefinition.NameMaxLength);
        builder.Property(x => x.Description)
            .HasMaxLength(ProductDefinition.DescriptionMaxLength);
        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(ProductDefinition.PricePrecision, ProductDefinition.PriceScale);
        builder.Property(x => x.Stock)
            .IsRequired();
    }
}
