namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityConstants.CustomOrder;

    public class CustomOrderConfiguration : IEntityTypeConfiguration<CustomOrder>
    {
        public void Configure(EntityTypeBuilder<CustomOrder> entity)
        {
            entity
                .HasKey(co => co.Id);
            entity
              .Property(co => co.UserName)
              .IsRequired(true);
            entity
              .Property(co => co.PhoneNumber)
              .IsRequired(true);
            entity
              .Property(co => co.Address)
              .IsRequired(true);
            entity
                .Property(co => co.RequestedDate)
                .IsRequired(true);

            entity
                .Property(co => co.Details)
                .IsRequired(true)
                .HasMaxLength(CustomOrderDetailsMaxLenght);
        }
    }
}
