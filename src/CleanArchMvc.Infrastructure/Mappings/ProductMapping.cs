using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infrastructure.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Products");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasMaxLength(254)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(254)
                .IsRequired();

            builder
                .Property(x => x.Price)
                .HasPrecision(10, 2)
                .IsRequired();

            builder
                .Property(x => x.Stock)
                .IsRequired();

            builder
                .Property(x => x.Image)
                .HasMaxLength(254);

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}