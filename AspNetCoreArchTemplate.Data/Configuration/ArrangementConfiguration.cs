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

            entity
                .HasData(this.SeedArragements());

        }
        private IEnumerable<Arrangement> SeedArragements()
        {
            List<Arrangement> arrangements = new List<Arrangement>()
            {
                new Arrangement
                {
                    Id = Guid.Parse("a3c30e0e-adf3-4a5b-ba9e-d32162c5b2d5"),
                    Name = "Elegant Wedding Arrangement",
                    Description = "A luxurious arrangement with white roses, peonies, and hydrangeas for weddings.",
                    Price = 120.00m,
                    ImageUrl = "https://www.theknot.com/tk-media/images/183bcf98-9b6d-11e4-843f-22000aa61a3e~rs_1458.h?quality=60",
                    EventType = "Wedding",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4") // Roses
                },
                new Arrangement
                {
                    Id = Guid.Parse("0a646a94-1c80-47b6-b1b4-0046a8c6b9b4"),
                    Name = "Birthday Joy Arrangement",
                    Description = "Gental tulips and daisies to brighten any birthday celebration.",
                    Price = 75.00m,
                    ImageUrl = "https://buket-express.ua/image/cache/catalog/b7fea839-fbbe-4ca5-b5ae-37511c1360a8-479x471.jpg",
                    EventType = "Birthday",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("d791f856-10f5-43fc-a64c-a01ecdd50b4c") // Tulips
                },
                new Arrangement
                {
                    Id = Guid.Parse("dc54ef1a-4fde-4e88-b6af-a3384ab26a18"),
                    Name = "Romantic Anniversary Arrangement",
                    Description = "Red roses, pink carnations, and baby’s breath for anniversaries.",
                    Price = 110.00m,
                    ImageUrl = "https://ovenfresh.in/wp-content/uploads/2023/02/Rose-Spray-Pink-Carnation-And-Baby-Breath_1-min.jpg",
                    EventType = "Anniversary",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4") // Roses
                },
                new Arrangement
                {
                    Id = Guid.Parse("5e1776d8-c706-4d6d-b033-3b8b24ad70b9"),
                    Name = "Gratitude Bloom Arrangement",
                    Description = "A joyful arrangement of cheerful gerberas, yellow roses, and vibrant daisies to express gratitude and positivity.",
                    Price = 95.00m,
                    ImageUrl = "https://ovenfresh.in/wp-content/uploads/2023/02/Fresh-Vibes-Arrangement-Of-Yellow-Gerberas-Roses-Daisies-In-A-Vase_1-min.jpg",
                    EventType = "Thank You",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("33494634-acb2-4a4f-b17f-84b7a509c615")
                },
                new Arrangement
                {
                    Id = Guid.Parse("46cf97cc-15e7-4278-b685-2de157dc060a"),
                    Name = "Graduation Party Arrangement",
                    Description = "Bright sunflowers, blue hydrangeas, and yellow roses to celebrate graduation.",
                    Price = 85.00m,
                    ImageUrl = "https://asset.bloomnation.com/c_pad,d_vendor:global:catalog:product:image.png,f_auto,fl_preserve_transparency,q_auto/v1747105267/vendor/1687/catalog/product/2/0/20200715022009_file_5f0f1099589f4_5f0f11c6d9ebd.jpg",
                    EventType = "Graduation",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("5801cc76-5a81-4277-be80-ad5bf3722b6f")
                }
            };
            return arrangements;
        }
    }
}
