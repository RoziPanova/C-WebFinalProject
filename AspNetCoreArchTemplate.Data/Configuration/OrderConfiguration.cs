namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityConstants.Order;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity
                .HasKey(o => o.Id);

            entity
                .Property(o => o.UserId)
                .IsRequired(true);

            entity
                .Property(o => o.Address)
                .IsRequired(true)
                .HasMaxLength(CustomerAddressMaxLength);

            entity
                .Property(o => o.CustomerName)
                .IsRequired(true)
                .HasMaxLength(CustomerNameMaxLength);

            entity
                .Property(o => o.OrderDate)
                .IsRequired(true);


            entity
                .HasOne(o => o.ApplicationUser)
                .WithMany(au => au.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
