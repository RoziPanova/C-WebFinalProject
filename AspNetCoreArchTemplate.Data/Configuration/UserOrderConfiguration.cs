namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrder>
    {
        public void Configure(EntityTypeBuilder<UserOrder> entity)
        {
            entity
                .HasKey(uo => new { uo.OrderItemId, uo.UserId });

            entity
                .Property(au => au.UserId)
                .IsRequired();

            entity
                .Property(au => au.OrderItemId)
                .IsRequired();

            entity
                .HasOne(uo => uo.User)
                .WithMany(u => u.UserOrders)
                .HasForeignKey(uo => uo.UserId);

            entity
                .HasOne(uo => uo.OrderItem)
                .WithMany(o => o.UserOrders)
                .HasForeignKey(uo => uo.OrderItemId);

        }
    }
}
