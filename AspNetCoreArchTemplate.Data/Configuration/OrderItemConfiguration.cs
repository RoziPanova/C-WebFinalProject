namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity
                .HasKey(oi => oi.Id);

            entity
                .Property(oi => oi.Quantity)
                .IsRequired(true);

            entity
                .HasOne(oi => oi.Bouquet)
                .WithMany(b => b.OrderItems)
                .HasForeignKey(oi => oi.BouquetId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
               .HasOne(oi => oi.Arrangement)
               .WithMany(a => a.OrderItems)
               .HasForeignKey(oi => oi.ArrangementId)
               .OnDelete(DeleteBehavior.Restrict);
            entity

               .HasOne(oi => oi.CustomOrder)
               .WithMany(co => co.OrderItems)
               .HasForeignKey(oi => oi.CustomOrderId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
