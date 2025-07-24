namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityConstants.Bouquet;
    using static GCommon.ApplicationConstants;

    public class BouquetConfiguration : IEntityTypeConfiguration<Bouquet>
    {
        public void Configure(EntityTypeBuilder<Bouquet> entity)
        {
            entity
                .HasKey(b => b.Id);

            entity
                .Property(b => b.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(b => b.Description)
                .IsRequired(false)
                .HasMaxLength(DescriptionMaxLength);

            entity
               .Property(b => b.Price)
               .IsRequired(true)
               .HasColumnType(PriceSqlType);

            entity
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(b => b.Category)
                .WithMany(c => c.Bouquets)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasMany(b => b.OrderItems)
                .WithOne(oi => oi.Bouquet)
                .HasForeignKey(oi => oi.BouquetId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(b => b.IsDeleted == false);
        }
    }
}
