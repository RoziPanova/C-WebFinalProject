namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityConstants.Arrangement;
    using static GCommon.ApplicationConstants;

    public class ArrangementConfiguration : IEntityTypeConfiguration<Arrangement>
    {
        public void Configure(EntityTypeBuilder<Arrangement> entity)
        {
            entity
                .HasKey(a => a.Id);

            entity
                .Property(a => a.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(a => a.Description)
                .IsRequired(true)
                .HasMaxLength(DescriptionMaxLength);

            entity.Property(a => a.Price)
                .IsRequired(true)
                .HasColumnType(PriceSqlType);

            entity
                .Property(a => a.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(a => a.Category)
                .WithMany(c => c.Arrangements)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(a => a.IsDeleted == false);

        }
    }
}
